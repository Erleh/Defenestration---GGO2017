using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    public AudioSource inGameMusicV1;

    void Start()
    {
        inGameMusicV1.Play();
    }
}
