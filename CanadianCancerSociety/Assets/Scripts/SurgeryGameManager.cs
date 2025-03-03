using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SurgeryGameManager : MonoBehaviour
{
    private List<GameObject> trackedObjects = new List<GameObject>(); // List to track GameObjects 
    public int surgeryProcess;
    public bool surgeryFinished = false;

    public float surgeryScore = 1500f;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(surgeryScore);
        TrackPositiveBoolObjects();
    }

    void TrackPositiveBoolObjects()
    {
        // Clear previous tracked objects
        trackedObjects.Clear();

        // Find all GameObjects with the target tag
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("OutLine");

        // Loop through all GameObjects and check the bool value in the ClickableObject script
        foreach (var obj in allObjects)
        {
            ClickableObject clickable = obj.GetComponent<ClickableObject>();
            if (clickable != null && clickable.isClicked)
            {
                // If the bool is true, add this object to the tracked list
                trackedObjects.Add(obj);
            }
        }

        surgeryProcess = trackedObjects.Count;

        if (surgeryProcess >= allObjects.Length)
        {
            if(!surgeryFinished)
            {
                Debug.Log("You Win");
            }
            surgeryFinished = true;
        }

    }

    public void IncreaseScore(float increaseAmount)
    {
        surgeryScore += increaseAmount;
    }
    public void DecreaseScore(float decreaseAmount)
    {
        surgeryScore -= decreaseAmount;
    }
}
