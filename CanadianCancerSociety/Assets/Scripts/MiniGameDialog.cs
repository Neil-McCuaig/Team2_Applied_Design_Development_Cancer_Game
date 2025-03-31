using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameDialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField, TextArea(3, 10)] public string[] lines;
    public float textSpeed;
    private int index;
    public AudioSource audioSource;
    public AudioClip clip;

    public bool triggerDialog = false;

    public Animator anim;
    AudioManager audioManager;

    private void Start()
    {
        anim.SetBool("isOpen", true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDialog)
        {
            audioSource.Play();
            triggerDialog = false;
            if (textComponent.text == lines[index])
            {
                NextLine();
                audioSource.Play();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void TriggerDialog()
    {
        triggerDialog = true;
    }

    public void ResetDialog()
    {
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
            anim.SetBool("isOpen", false);
        }
    }
}
