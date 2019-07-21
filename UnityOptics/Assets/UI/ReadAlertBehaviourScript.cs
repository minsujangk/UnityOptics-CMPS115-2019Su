using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class ReadAlertBehaviourScript : MonoBehaviour
{
    GameObject[] adObjects;
    public GameObject readAlert;

    Sprite activeReadingAdSprite;
    string activeReadingAdName;

    public GameObject AdDescription;
    public GameObject AdImageView;

    ReadListWrapper rlistw = new ReadListWrapper();
    ReadTrackData curTrackData;


    // Start is called before the first frame update
    void Start()
    {
        adObjects = GameObject.FindGameObjectsWithTag("AdObject");
    }

    // Update is called once per frame
    void Update()
    {
        bool isActive = false;
        var camPos = Camera.main.transform.position;
        float activeReadingAdDistance = 10.0F;

        foreach (var adObject in adObjects){
            AdBehaviorScript sc = adObject.GetComponent<AdBehaviorScript>();
            bool isDrawReadAlert = sc.isDrawReadAlert;

            //Debug.Log(adObject.name + " in read is " + isDrawReadAlert);

            if (isDrawReadAlert)
            {
                if (activeReadingAdDistance > sc.adDistance)
                {
                    activeReadingAdSprite = sc.AdSprite;
                    activeReadingAdDistance = sc.adDistance;
                    activeReadingAdName = adObject.name;
                }
            }

            isActive |= isDrawReadAlert;

        }

        readAlert.SetActive(isActive);
        if (!isActive)
        {
            activeReadingAdSprite = null;
            activeReadingAdDistance = 10.0F;
            activeReadingAdName = null;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleActiveAd();
        }
    }

    string out_folder = "Assets/Output/";
    void OnDestroy()
    {

        // Write and save game data to .json file 
        string saveText = JsonUtility.ToJson(rlistw);

        Debug.Log("readAler: " + saveText);

        string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");


        if (!Directory.Exists(out_folder))
            Directory.CreateDirectory(out_folder);

        if (!Directory.Exists(out_folder + "read/"))
            Directory.CreateDirectory(out_folder + "read/");

        string filename = "read_" + datetime + ".json";
        string local_filepath = out_folder + "read/" + filename;
        File.WriteAllText(local_filepath, saveText); // TODO @Matthew: This takes 2 arguments, the filepath and the json saveText var

        // Get a reference to Firebase cloud storage service
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;

        // Create storage reference from our storage service bucket
        Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl("gs://unityoptics-eafc0.appspot.com");

        //  Create a reference to newly created .json file
        Firebase.Storage.StorageReference game_data_ref = storage_ref.Child(filename);

        // Create reference to 'gameData/filename'
        Firebase.Storage.StorageReference game_data_json_ref =
            storage_ref.Child("gameData/" + "read/" + filename);

        // Upload Files to Cloud FireStore
        game_data_json_ref.PutFileAsync(local_filepath)
           .ContinueWith((System.Threading.Tasks.Task<StorageMetadata> task) => {
               if (task.IsFaulted || task.IsCanceled)
               {
                   Debug.Log(task.Exception.ToString());
                   // Error Occured
               }
               else
               {
                   //Metadata contains file metadata such as size, content-type, and download URL.
                   Firebase.Storage.StorageMetadata metadata = task.Result;
                   // string download_url = metadata.DownloadUrl.ToString();
                   Debug.Log("Finished uploading...");
                   // Debug.Log("download url = " + download_url);
               }
           });
    }

    void toggleActiveAd()
    {
        if (!AdDescription.activeSelf && activeReadingAdSprite != null)
        {
            AdImageView.GetComponent<Image>().sprite = activeReadingAdSprite;
        }
        AdDescription.SetActive(!AdDescription.activeSelf);

        if (AdDescription.activeSelf)
        {
            curTrackData = new ReadTrackData();
            curTrackData.objName = activeReadingAdName;
            curTrackData.time = Time.time;
            rlistw.data.Add(curTrackData);
        }
        else
        {
            curTrackData.duration = Time.time - curTrackData.time;
            curTrackData = null;
        }
    }

    [System.Serializable]
    public class ReadTrackData
    {
        public string objName;
        public float time;
        public float duration;

    }

    [System.Serializable]
    public class ReadListWrapper
    {
        public List<ReadTrackData> data = new List<ReadTrackData>();
    }
}
