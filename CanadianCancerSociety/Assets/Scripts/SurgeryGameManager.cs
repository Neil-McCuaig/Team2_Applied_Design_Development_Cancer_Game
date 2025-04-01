using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SurgeryGameManager : MonoBehaviour
{
    private List<GameObject> trackedObjects = new List<GameObject>(); // List to track GameObjects 
    public int surgeryProcess;
    public bool surgeryFinished = false;

    public float surgeryScore = 1500f;
    public TextMeshProUGUI scoreText;

    MiniGameDialog dialog;
    public GameObject winScreen;

    public TextMeshProUGUI timerText;
    private float currentTime = 0;

    void Start()
    {
        dialog = FindAnyObjectByType<MiniGameDialog>();
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = ("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));

        scoreText.text = "Score: " + surgeryScore;
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
                winScreen.SetActive(true);
            }
            surgeryFinished = true;
        }
    }
    public void CallForDialog()
    {
        if (surgeryProcess == 10)
        {
            dialog.TriggerDialog();
        }
        else if (surgeryProcess == 20)
        {
            dialog.TriggerDialog();
        }
        else if (surgeryProcess == 30)
        {
            dialog.TriggerDialog();
        }
        else if (surgeryProcess == 40)
        {
            dialog.TriggerDialog();
        }
        else if (surgeryProcess == 50)
        {
            dialog.TriggerDialog();
        }
        else if (surgeryProcess == 60)
        {
            dialog.TriggerDialog();
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
