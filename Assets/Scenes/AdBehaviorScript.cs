using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class AdBehaviorScript : MonoBehaviour
{
    GameObject player;
    Renderer m_Renderer;
    
    ViewData curData = null;
    ListWrapper listw = new ListWrapper();

    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
        m_Renderer = GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // getting position of object
        var objPos = gameObject.transform.position;
        var camPos = Camera.main.transform.position;
        
        var cameraNormal = Camera.main.transform.forward;
        float objAngle = Vector3.Angle(cameraNormal, objPos - camPos);


        Debug.Log(gameObject.name + " pos: " + objPos.ToString("G4") + ", Time: "
            + Time.time + ", Player pos: " + camPos.ToString("G4"));

        if (m_Renderer.isVisible)
        {

            RaycastHit hit;
            // Calculate Ray direction
            Vector3 direction = camPos - objPos;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                Debug.Log(gameObject.name + " is occluded by " + hit.collider.name);
            }
            else
            {
                float distance = Vector3.Distance(objPos, camPos);

                if (curData == null)
                {
                    curData = new ViewData();
                    curData.objName = gameObject.name;
                    curData.minDist = distance;
                    curData.maxDist = distance;
                    curData.time = Time.time;
                    listw.data.Add(curData);
                }
                curData.duration = Time.time - curData.time;

                if (distance < curData.minDist)
                    curData.minDist = distance;
                if (distance > curData.maxDist)
                    curData.maxDist = distance;


                Debug.Log(gameObject.name + ": visible, Duration:" + curData.duration + "s"
                    + ", Dist: " + distance.ToString("G4") + ", Angle: " + objAngle);
            }
        }
        else
        {
            if (curData != null)
            {
                // this view had been viewed by player
                curData = null;
            }

            Debug.Log(gameObject.name + ": not visible");
        }
    }

    string out_folder = "Assets/Output/";
    void OnDestroy()
    {        
        string saveText = JsonUtility.ToJson(listw);
        
        string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
        
        if (!Directory.Exists(out_folder))
            Directory.CreateDirectory(out_folder);
        File.WriteAllText("Assets/Output/save_" + datetime + "_" + gameObject.name + ".txt", saveText + "]");

        ///////////// FIRE STORE DATA STORAGE INCOMPLETE ////////////////

        // Write and save game data to .json file 
       // string saveText = JsonUtility.ToJson(listw);
        
      //  string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
        
      //  if (!Directory.Exists(out_folder))
       //     Directory.CreateDirectory(out_folder);
     //   string filename = "save_" + datetime + "_" + gameObject.name + ".json" + saveText + "]";
      //  string local_filepath = "Assets/Output/" + filename;
      //  File.WriteAllText(local_filepath);

        // Get a reference to Firebase cloud storage service
        //Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance; 

        // Create storage reference from our storage service bucket
       // Firebase.Storage.StorageReference storage_ref =
        //    storage.GetReferenceFromURL("gs://unityoptics-eafc0.appspot.com");

        // Create a reference to newly created .json file
        //Firebase.Storage.StorageReference game_data_ref = storage_ref.Child(filename);

       // Create reference to 'gameData/filename'
       //Firebase.Storage.StorageReference game_data_json_ref = 
         //   storage_ref.Child("gameData/" + filename);

        // Upload Files to Cloud FireStore
        //game_data_json_ref.PutFileAsync(local_filepath)
          //  .ContinueWith ((Task<StorageMetadata> Task) => {
            //    if (task.IsFaulted || task.IsCanceled) {
              //      Debug.Log(task.Exception.ToString());
                    // Error Occured
               // } else {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                 //   Firebase.Storage.StorageMetadata metadata = task.Result;
                   // string download_url = metadata.DownloadUrl.ToString();
                  //  Debug.Log("Finished uploading...");
                //    Debug.Log("download url = " + download_url);
              //  }
           // });
    }

    [System.Serializable]
    public class ViewData
    {
        public string objName;
        public float time;
        public float duration;
        public float minDist;
        public float maxDist;
            
    }

    [System.Serializable]
    public class ListWrapper
    {
        public List<ViewData> data = new List<ViewData>();
    }
}
