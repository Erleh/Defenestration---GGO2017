using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource kick;
    public AudioSource shove;
    public AudioSource run;

    public void KickSound()
    {
        kick.Play();
    }

    public void RunSound()
    {
        run.Play();
    }

    public void ShoveSound()
    {
        shove.Play();
    }
}
