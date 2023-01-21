using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clickSound;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audiosource.PlayOneShot(clickSound);     
    }
}
