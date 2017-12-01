using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundScript : MonoBehaviour
{
    public AudioSource menuMusic1;
    public AudioSource menuMusic2;

    public AudioSource playedCurrently;

    void Start()
    {
        float ran = Random.Range(-2, 2);

        if (ran > 0)
        {
            playedCurrently = menuMusic1;

            Debug.Log("1 s");
            menuMusic1.Play();
        }
        else
        {
            playedCurrently = menuMusic2;

            Debug.Log("2 s");
            menuMusic2.Play();
        }
    }
}
