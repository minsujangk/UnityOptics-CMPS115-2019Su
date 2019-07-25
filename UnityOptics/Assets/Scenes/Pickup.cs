using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
// In this script, we decided to add more interactions for user
// we have already had several interactions which are reading Ad 
// on the object and keep tract of time and position and send them
// to firebase that we created

public class Pickup : MonoBehaviour
{
	//setting up variable including player and game object
    float throwForce = 600;																		
    Vector3 objectPos;																	
    float distance;																				
    public bool canHold = true;																	
    public GameObject item;																		
    public GameObject tempParent;																
    public bool isHolding = false;																

    PickupTrackData td;

    PickupListWrapper listw = new PickupListWrapper();
    void Update()
    {
    	//checking if the distance is close enough for grabbing game object
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position); 
        //initialize holding is false	
        if(distance >= 1f)																		
        {
            isHolding = false; 																	
        }
        if(isHolding == true) 																	
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;								
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 						
            item.transform.SetParent(tempParent.transform); 					
            //adding right mouse click as throwing objects while users grabbing objects		
            if (Input.GetMouseButtonDown(1))													
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;                                                             
                
                PickupTrackData tdd = new PickupTrackData();
                tdd.objName = item.name;
                tdd.time = Time.time;
                tdd.type = 2; // throwing = 2
                listw.data.Add(tdd);

                if (td != null) td = null;
            }
        }
        else
        //if isHolding is false the game object stays the same place									
        {
            objectPos = item.transform.position;												
            item.transform.SetParent(null);														
            item.GetComponent<Rigidbody>().useGravity = true;									
            item.transform.position = objectPos;
        }
        if (distance<=1f)
        {	
        	//new feature key press E to push objects																					
        	if (Input.GetKeyDown(KeyCode.E))
         	{
                item.transform.SetParent(tempParent.transform);
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);

                PickupTrackData tdd = new PickupTrackData();
                tdd.objName = item.name;
                tdd.time = Time.time;
                tdd.type = 2; // throwing = 2
                listw.data.Add(tdd);
                if (td != null) td = null;
            }
        }
    }
	//left mouse click enable user to grab
    void OnMouseDown()																			
    {
        if (distance <= 1f)																		
        {
            isHolding = true;																	
            item.GetComponent<Rigidbody>().useGravity = false;									
            item.GetComponent<Rigidbody>().detectCollisions = true;								

            if (td == null)
            {
                td = new PickupTrackData();
                td.objName = item.name;
                td.time = Time.time;
                td.type = 1; // pickup = 1
                listw.data.Add(td);
            }
        }
    }
	//turning holding is false
    void OnMouseUp()																			
    {
        isHolding = false;																		
        td = null;
    }

    string out_folder = "Assets/Output/";
    void OnDestroy()
    {

        // Write and save game data to .json file 
        string saveText = JsonUtility.ToJson(listw);

        Debug.Log("readAler: " + saveText);

        string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");


        if (!Directory.Exists(out_folder))
            Directory.CreateDirectory(out_folder);

        if (!Directory.Exists(out_folder + gameObject.name + "/" + "pickup/"))
            Directory.CreateDirectory(out_folder + gameObject.name + "/" + "pickup/");

        string filename = "pickup_" + datetime + ".json";
        string local_filepath = out_folder + gameObject.name + "/" + "pickup/" + filename;
        File.WriteAllText(local_filepath, saveText); // TODO @Matthew: This takes 2 arguments, the filepath and the json saveText var

        // Get a reference to Firebase cloud storage service
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;

        // Create storage reference from our storage service bucket
        Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl("gs://unityoptics-eafc0.appspot.com");

        //  Create a reference to newly created .json file
        Firebase.Storage.StorageReference game_data_ref = storage_ref.Child(filename);

        // Create reference to 'gameData/filename'
        Firebase.Storage.StorageReference game_data_json_ref =
            storage_ref.Child("gameData/" + gameObject.name + "/" + "pickup/" + filename);

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


    [System.Serializable]
    public class PickupTrackData
    {
        public string objName;
        public float time;
        public int type;

    }
    
    [System.Serializable]
    public class PickupListWrapper
    {
        public List<PickupTrackData> data = new List<PickupTrackData>();
    }
}
