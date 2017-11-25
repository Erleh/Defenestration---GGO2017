using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimationHandler : MonoBehaviour
{

    public delegate void push(GameObject character);
    public delegate void shove(GameObject character);
    public delegate void kick(GameObject character);

    public static event push onPush;
    public static event shove onShove;
    public static event kick onKick;

    public void OnCharacterPush(GameObject character)
    {
        if(onPush != null)
        {
            onPush(character);
        }

    }

    public void OnCharacterShove(GameObject character)
    {
        if(onShove != null)
        {
            onShove(character);
        }
    }

    public void OnCharacterKick(GameObject character)
    {
        if(onKick != null)
        {
            onKick(character);
        }
    }
}
