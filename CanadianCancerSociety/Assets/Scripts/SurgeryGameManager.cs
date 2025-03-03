using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeryGameManager : MonoBehaviour
{
    private List<GameObject> trackedObjects = new List<GameObject>(); // List to track GameObjects 
    public int surgeryProcess;
    public bool surgeryFinished = false;

    // Update is called once per frame
    void Update()
    {
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
}
