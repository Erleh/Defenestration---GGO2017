using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioSource kick1;
    public AudioSource kick2;
    public AudioSource shove;
    public AudioSource run;
    float rando = 0;
    public void KickSound()
    {
       rando = Random.Range(0f, 1f);
        if (rando < .5f)
        {
            kick1.Play();
        }
        else
        {
            kick2.Play();
        }
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
