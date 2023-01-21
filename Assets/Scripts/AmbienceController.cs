using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    public AudioSource audioSource;
    public static AmbienceController instance;
    public AudioClip ambience;
    public AudioClip gameOver;
    public AudioClip gameWin;
    public AudioClip menuAmbience;
    public AudioClip lavaAmbience;
    public AudioClip nightAmbience;

    private void Awake()
    {      
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(string musicName)
    {
        audioSource.Stop();
        switch (musicName)
        {
            case "Ambience":
                audioSource.clip = ambience;
                audioSource.Play();
                break;

            case "LavaAmbience":
                audioSource.clip = lavaAmbience;
                audioSource.Play();
                break;

            case "NightAmbience":
                audioSource.clip = nightAmbience;
                audioSource.Play();
                break;

            case "GameOver":           
                audioSource.PlayOneShot(gameOver);
                break;

            case "GameWin":        
                audioSource.PlayOneShot(gameWin);
                break;            

            case "Menu":
                audioSource.clip = menuAmbience;
                audioSource.Play();
                break;
        }
       
    }
}
