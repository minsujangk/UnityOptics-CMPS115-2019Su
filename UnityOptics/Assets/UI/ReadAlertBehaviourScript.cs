using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadAlertBehaviourScript : MonoBehaviour
{
    GameObject[] adObjects;
    public GameObject readAlert;

    Sprite activeReadingAdSprite;

    public GameObject AdDescription;
    public GameObject AdImageView;

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
                }
            }

            isActive |= isDrawReadAlert;

        }

        readAlert.SetActive(isActive);
        if (!isActive)
        {
            activeReadingAdSprite = null;
            activeReadingAdDistance = 10.0F;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleActiveAd();
        }
    }

    void toggleActiveAd()
    {
        if (!AdDescription.activeSelf && activeReadingAdSprite != null)
        {
            AdImageView.GetComponent<Image>().sprite = activeReadingAdSprite;
        }
        AdDescription.SetActive(!AdDescription.activeSelf);
    }
}
