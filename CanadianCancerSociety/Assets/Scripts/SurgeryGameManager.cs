using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SurgeryGameManager : MonoBehaviour
{
    private List<GameObject> trackedObjects = new List<GameObject>(); // List to track GameObjects 
    public int surgeryProcess;
    public bool surgeryFinished = false;

    public int tumorStruck = 4;
    public TextMeshProUGUI scoreText;

    MiniGameDialog dialog;
    public GameObject winScreen;
    public GameObject loseScreen;
    bool hasLost = false;

    public TextMeshProUGUI timerText;
    private float currentTime = 0;

    public Animator anim;
    AudioManager audioManager;
    FadeOut fade;

    void Start()
    {
        dialog = FindAnyObjectByType<MiniGameDialog>();
        audioManager = FindAnyObjectByType<AudioManager>();
        fade = FindAnyObjectByType<FadeOut>();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = ("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));

        scoreText.text = "Mistakes Remaining: " + tumorStruck;
        TrackPositiveBoolObjects();

        if(tumorStruck == 3)
        {
            anim.SetInteger("Health", 1);
        }
        else if (tumorStruck == 2)
        {
            anim.SetInteger("Health", 2);
        }
        else if (tumorStruck == 1)
        {
            anim.SetInteger("Health", 3);
        }
        else if (tumorStruck == 0)
        {
            loseScreen.SetActive(true);
            hasLost = true;
        }
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
            if (clickable != null && clickable.isClicked && !hasLost)
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
            audioManager.PlaySFX(audioManager.popSound);
        }
        else if (surgeryProcess == 20)
        {
            dialog.TriggerDialog();
            audioManager.PlaySFX(audioManager.popSound);
        }
        else if (surgeryProcess == 30)
        {
            dialog.TriggerDialog();
            audioManager.PlaySFX(audioManager.popSound);
        }
        else if (surgeryProcess == 40)
        {
            dialog.TriggerDialog();
            audioManager.PlaySFX(audioManager.popSound);
        }
        else if (surgeryProcess == 50)
        {
            dialog.TriggerDialog();
            audioManager.PlaySFX(audioManager.popSound);
        }
        else if (surgeryProcess == 60)
        {
            dialog.TriggerDialog();
            audioManager.PlaySFX(audioManager.popSound);
        }
    }

    public void DecreaseScore()
    {
        tumorStruck -= 1;
        audioManager.PlaySFX(audioManager.hurt);
    }

    public void NextLevel()
    {
        fade.StartFade();
        Invoke("LoadNextLevel", 5f);
    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level5");
    }
}
