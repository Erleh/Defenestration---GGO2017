using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    public AudioSource inGameMusicV1;
    public AudioSource inGameMusicV2;

    public AudioSource currMusic;

    void Start()
    {
        float ran = Random.Range(-2, 2);

        if (ran > 0)
        {
            currMusic = inGameMusicV1;

            inGameMusicV1.Play();
        }
        else
        {
            currMusic = inGameMusicV2;
            inGameMusicV2.Play();
        }
    }

    public AudioSource GetCurrMusic()
    {
        return currMusic;
    }
}
