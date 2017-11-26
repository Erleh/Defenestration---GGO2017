using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimationHandler : MonoBehaviour
{

    public delegate void push();
    public delegate void shove();
    public delegate void kick();

    public static event push onPush;
    public static event shove onShove;
    public static event kick onKick;

    public GameObject character;
    private Guard player;

    public void OnCharacterPush()
    {
        if(onPush != null)
        {
            onPush();
        }

    }

    public void OnCharacterShove()
    {
        if(onShove != null)
        {
            onShove();
        }
    }

    public void OnCharacterKick()
    {
        if(onKick != null)
        {
            onKick();
        }
    }

    void FixedUpdate()
    {
        if(character.GetComponent<Guard>() != false)
        {
            OnCharacterPush();
            OnCharacterShove();
            OnCharacterKick();
        }
    }
}
