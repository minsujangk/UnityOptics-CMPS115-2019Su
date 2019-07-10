using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;

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
