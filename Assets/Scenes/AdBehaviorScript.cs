using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBehaviorScript : MonoBehaviour
{
    GameObject player;
    Renderer m_Renderer;
    float initialVisibleTime = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
        m_Renderer = GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");

    }

    void Awake()
    { }

    // Update is called once per frame
    void Update()
    {
        // getting position of object
        var objPos = gameObject.transform.position;
        var camPos = Camera.main.transform.position;
        Debug.Log(gameObject.name + " pos: " + objPos.ToString("G4") + ", Time: " 
            + Time.time + ", Player pos: " + camPos.ToString("G4"));

        var cameraNormal = Camera.main.transform.forward;
     
        float objAngle = Vector3.Angle(cameraNormal, objPos - camPos);

        if (m_Renderer.isVisible)
        {
            float distance = Vector3.Distance(objPos, camPos);
            Debug.Log(gameObject.name + " is visible for " + (Time.time - initialVisibleTime) + "s"
                + ", Dist: " + distance.ToString("G4") + ", Angle: " + objAngle);
        }
        else
        {
            initialVisibleTime = Time.time;
            Debug.Log(gameObject.name + " is no longer visible");
        }
    }
}
