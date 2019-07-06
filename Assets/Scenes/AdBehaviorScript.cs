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
        var position = gameObject.transform.position;
        Debug.Log(gameObject.name + " pos: " + position.ToString("G4") + ", Time: " 
            + Time.time + ", Player pos: " + player.transform.position.ToString("G4"));

        if (m_Renderer.isVisible)
        {
            Debug.Log(gameObject.name + " is visible for " + (Time.time - initialVisibleTime) + "s");
        }
        else
        {
            initialVisibleTime = Time.time;
            Debug.Log(gameObject.name + " no longer visible");
        }
    }
}
