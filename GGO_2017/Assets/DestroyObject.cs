using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject obj;

    public AudioSource explosion;
    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void Destroy()
    {
        Destroy(obj);
    }
}
