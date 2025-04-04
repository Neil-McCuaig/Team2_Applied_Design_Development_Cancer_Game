using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea]
    public string[] lines;
    public float textSpeed;

    private int index;

    public bool isSpeaking;
    public GameObject continueButton;

    public Image char1Icon;
    public Image char2Icon;

    public Animator anim;

    public PlayerMovement player;

    AudioManager audioManager;

    FadeOut fade;

    private void Start()
    {
        continueButton.SetActive(false);
        player = FindAnyObjectByType<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get audio manager
        fade = FindAnyObjectByType<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSpeaking)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        if (index % 2 == 0)
        {
            char1Icon.enabled = true;
            char2Icon.enabled = false;
        }
        else
        {
            char2Icon.enabled = true;
            char1Icon.enabled = false;
        }
    }

    public void ResetDialog()
    {
        audioManager.PlaySFX(audioManager.popSound);
        player.inDialog = true;
        isSpeaking = true;
        anim.SetBool("isOpen", true);
        textComponent.text = string.Empty;
        StartDialogue();
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            audioManager.PlaySFX(audioManager.manGrunt);
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            player.inDialog = false;
            isSpeaking = false;
            anim.SetBool("isOpen", false);
            continueButton.SetActive(true);
        }
    }

    public void BeginImmunotherapy()
    {
        fade.StartFade();
        Invoke("ImmunotherapyLevel", 5f);
    }

    void ImmunotherapyLevel()
    {
        SceneManager.LoadScene("ImmunotherapyMiniGame");
    }

    public void BeginScanner()
    {
        fade.StartFade();
        Invoke("ScannerLevel", 5f);
    }

    void ScannerLevel()
    {
        SceneManager.LoadScene("ScannerMiniGame");
    }

    public void BeginNextPatient()
    {
        fade.StartFade();
        Invoke("NextPatient", 5f);
    }

    void NextPatient()
    {
        SceneManager.LoadScene("Level4");
    }

    public void BeginSurgery()
    {
        fade.StartFade();
        Invoke("SurgeryLevel", 5f);
    }

    void SurgeryLevel()
    {
        SceneManager.LoadScene("SurgeryMiniGame");
    }

    public void BeginDemoEnd()
    {
        fade.StartFade();
        Invoke("DemoEndLevel", 5f);
    }

    void DemoEndLevel()
    {
        SceneManager.LoadScene("EndOfDemo");
    }
}
