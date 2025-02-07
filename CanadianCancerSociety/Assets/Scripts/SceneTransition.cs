using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void PlayScanner()
    {
        SceneManager.LoadScene("ScannerMiniGame");
    }
    public void PlayImmunotherapy()
    {
        SceneManager.LoadScene("ImmunotherapyMiniGame");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
