using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public bool isSpeaking;

    public Image char1Icon;
    public Image char2Icon;

    public Animator anim;

    public PlayerMovement player;

    AudioManager audioManager;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Get audio manager
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
        }
    }
}
