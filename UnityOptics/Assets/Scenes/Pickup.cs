using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class Pickup : MonoBehaviour
{

    float throwForce = 600;																		//throwing object distance on unity
    Vector3 objectPos;																			//new variable Object Position
    float distance;																				//new float variable distance
    public bool canHold = true;																	//we first set the canHold boolean variable to true which means that the object can be held by user
    public GameObject item;																		//load GameObject 
    public GameObject tempParent;																//load Holder Object
    public bool isHolding = false;																//first set isHolding is false which means that users are not holding anything right now

    PickupTrackData td;

    PickupListWrapper listw = new PickupListWrapper();
    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position); 	//we first get user's current distance and store it in Distance variable
        if(distance >= 1f)																		//if the distance between users and object is less than certain number
        {
            isHolding = false; 																	//we set the isHolding to false which means that users cannot hold any objects because it is too far for users to pick
        }
        if(isHolding == true) 																	//if we detect that isHolding is true which means that our user is currently picking up the GameObject
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;								//we set velocity to zero
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 						// also angularVelocity to zero both of them are preventing GammeObject from floating around
            item.transform.SetParent(tempParent.transform); 									// the word transform means that we can move Gameobject around: now tempParent is user
            if (Input.GetMouseButtonDown(1))													//inside this function, if we simply right click mouse, it will throw the object
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce); //we add force to gameobject in the direction that user is facing
                isHolding = false;                                                              // because our object is thrown by user so it is reasonable for us to set isHolding back to false
                
                PickupTrackData tdd = new PickupTrackData();
                tdd.objName = item.name;
                tdd.time = Time.time;
                tdd.type = 2; // throwing = 2
                listw.data.Add(tdd);

                if (td != null) td = null;
            }
        }
        else																					//if users are not holding the gameObject, we will do..
        {
            objectPos = item.transform.position;												//gameObject still stays the same place
            item.transform.SetParent(null);														//No user Parent is set
            item.GetComponent<Rigidbody>().useGravity = true;									//Gravity is still legit
            item.transform.position = objectPos;
        }
        if (distance<=1f)
        {																		//user can press E to push object which is the same as throwing
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

    void OnMouseDown()																			//OnMouseDown function which is saying :hold left mouse click
    {
        if (distance <= 1f)																		//if distance is close enough, we will do...
        {
            isHolding = true;																	//first we know that user is able to pick up the GameObject, so we set isHolding to ture
            item.GetComponent<Rigidbody>().useGravity = false;									//Gravity is disable because user is picking up the GameObject
            item.GetComponent<Rigidbody>().detectCollisions = true;								//detectCollisions means that we can detect the collisions between the GameObject that we are holding and the other gameObject

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

    void OnMouseUp()																			//mouse right click
    {
        isHolding = false;																		//in this case, because we are throwing the object, we will let our object go, so set isHolding to false
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
