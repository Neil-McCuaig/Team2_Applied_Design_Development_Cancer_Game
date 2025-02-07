using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XrayMiniGameManager : MonoBehaviour
{
    public bool targetFound = false;
    public TextMeshProUGUI boxText;

    private bool buttonPressed = false;
    private bool isInTriggerZone = false;


    void Start()
    {
        boxText.text = "Search for the cancer cell";
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
        if (targetFound)
        {
            boxText.text = "You win";
            Invoke("LoadNext", 1f);
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
    }

    void LoadNext()
    {
        SceneManager.LoadScene("ImmunotherapyMiniGame");
    }
}
