using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdCameraBehavior : MonoBehaviour
{
    List<Renderer> renderers = new List<Renderer>();
    GameObject[] adObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        adObjects = GameObject.FindGameObjectsWithTag("AdObject");
        foreach (var adObject in adObjects) {
            var rs = adObject.GetComponentsInChildren<Renderer>();
            foreach (var r in rs) {
                renderers.Add(r);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var renderer in renderers)
        {
            if (renderer.isVisible)
            {
                Debug.Log(renderer.name + "hi!");
            }
        }
    }
}
