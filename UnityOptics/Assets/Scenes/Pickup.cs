using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    float throwForce = 600;																		//throwing object distance on unity
    Vector3 objectPos;																			//new variable Object Position
    float distance;																				//new float variable distance
    public bool canHold = true;																	//we first set the canHold boolean variable to true which means that the object can be held by user
    public GameObject item;																		//load GameObject 
    public GameObject tempParent;																//load Holder Object
    public bool isHolding = false;																//first set isHolding is false which means that users are not holding anything right now
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
                isHolding = false; 																// because our object is thrown by user so it is reasonable for us to set isHolding back to false
            }
        }
        else																					//if users are not holding the gameObject, we will do..
        {
            objectPos = item.transform.position;												//gameObject still stays the same place
            item.transform.SetParent(null);														//No user Parent is set
            item.GetComponent<Rigidbody>().useGravity = true;									//Gravity is still legit
            item.transform.position = objectPos;
        }
    }

    void OnMouseDown()																			//OnMouseDown function which is saying :hold left mouse click
    {
        if (distance <= 1f)																		//if distance is close enough, we will do...
        {
            isHolding = true;																	//first we know that user is able to pick up the GameObject, so we set isHolding to ture
            item.GetComponent<Rigidbody>().useGravity = false;									//Gravity is disable because user is picking up the GameObject
            item.GetComponent<Rigidbody>().detectCollisions = true;								//detectCollisions means that we can detect the collisions between the GameObject that we are holding and the other gameObject
        }
    }

    void OnMouseUp()																			//mouse right click
    {
        isHolding = false;																		//in this case, because we are throwing the object, we will let our object go, so set isHolding to false
    }
}
