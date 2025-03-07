using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void PlayScanner()
    {
        SceneManager.LoadScene("ScannerMiniGame");
    }
    public void PlayImmunotherapy()
    {
        SceneManager.LoadScene("ImmunotherapyMiniGame");
    }
    public void PlaySurgery()
    {
        SceneManager.LoadScene("SurgeryMiniGame");
    }
    public void AfterCredits()
    {
        SceneManager.LoadScene("EndOfDemo");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
