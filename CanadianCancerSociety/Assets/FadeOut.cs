using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public GameObject fadeOut;

    private bool fadeStart = false;

    private void Start()
    {
        fadeOut.SetActive(false);
    }

    public void StartFade()
    {
        if (!fadeStart)
        {
            fadeStart = true;
            fadeOut.SetActive(true);
        }
    }

}
