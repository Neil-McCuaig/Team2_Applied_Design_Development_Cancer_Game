using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PauseMenu pauseMenu;

    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource SFXSourceTwo; 

    [Header("----------- Audio Clips -----------")]
    public AudioClip backgroundMusic;
    public AudioClip popSound;
    public AudioClip manGrunt;
    public AudioClip penLong;
    public AudioClip walking;


    private void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void Update()
    {
        if (pauseMenu.isPaused)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.UnPause();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySFX2(AudioClip clip)
    {
        SFXSourceTwo.PlayOneShot(clip);
    }
}
