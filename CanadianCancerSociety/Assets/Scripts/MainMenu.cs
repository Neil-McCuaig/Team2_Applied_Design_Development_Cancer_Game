using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(DelayForAudio());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayForAudio() // Countdown till draw timer
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Level1");
    }
}
