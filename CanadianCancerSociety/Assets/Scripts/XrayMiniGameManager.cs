using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XrayMiniGameManager : MonoBehaviour
{
    public bool targetFound = false;

    private bool buttonPressed = false;
    private bool isInTriggerZone = false;

    public Transform[] spawnPoints; // Array of transforms where we want to spawn the cancer
    public GameObject cancerCell; // The object to spawn

    MiniGameDialog dialog;
    AudioManager audioManager;
    FadeOut fade;

    private void Start()
    {
        dialog = FindAnyObjectByType<MiniGameDialog>();
        audioManager = FindAnyObjectByType<AudioManager>();
        fade = FindAnyObjectByType<FadeOut>();
        SpawnObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player enters trigger zone, set isInTriggerZone to true
        if (other.CompareTag("Target")) // Ensure the target has the "Target" tag
        {
            isInTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If player exits the trigger zone, set isInTriggerZone to false
        if (other.CompareTag("Target"))
        {
            isInTriggerZone = false;
        }
    }

    void Update()
    {
        // Check if the player presses the button 
        if (isInTriggerZone && buttonPressed)
        {
            targetFound = true; // Set the bool to true
        }
    }

    void SpawnObject()
    {
        // Check if spawnPoints array is not empty
        if (spawnPoints.Length > 0)
        {
            // Randomly choose an index from the spawnPoints array
            int randomIndex = Random.Range(0, spawnPoints.Length);

            // Get the random spawn position
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the object at the selected spawn point's position and rotation
            Instantiate(cancerCell, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void ButtonPress()
    {
        buttonPressed = true;
        Invoke("ResetButton", 0.5f);
    }

    void ResetButton()
    {
        buttonPressed = false;
        if(!targetFound)
        {
            audioManager.PlaySFX(audioManager.incorrect);
        }
        else
        {
            dialog.TriggerDialog();
            Invoke("TriggerFade", 17f);
            Invoke("LoadNext", 20f);
            audioManager.PlaySFX(audioManager.popSound);
        }
    }

    void LoadNext()
    {
        SceneManager.LoadScene("Level2");
    }
    void TriggerFade()
    {
        fade.StartFade();
    }
}
