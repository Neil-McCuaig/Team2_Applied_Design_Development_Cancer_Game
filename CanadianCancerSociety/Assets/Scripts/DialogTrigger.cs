using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public TextMeshProUGUI interactText;

    private bool canSpeak = false;


    private void Start()
    {
        dialog = FindAnyObjectByType<Dialog>();
        interactText.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSpeak = true;
            interactText.enabled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canSpeak && Input.GetMouseButton(0))
        {
            canSpeak = false;
            interactText.enabled = false;
            dialog.ResetDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canSpeak = false;
        if (interactText != null)
        {
            interactText.enabled = false;
        }
    }
}
