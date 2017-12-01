using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimationHandler : MonoBehaviour
{

    public delegate void push();
    public delegate void shove();
    public delegate void kick();
    public delegate void win();
	public delegate void lost();

    public static event push onPush;
    public static event shove onShove;
    public static event kick onKick;
    public static event win onWin;
	public static event lost onLose;

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

    public void OnWin()
    {
        if(onWin != null)
        {
            onWin();
        }
    }

	public void OnLose()
	{
		if(onLose != null)
		{
			onLose();
		}
	}

    void FixedUpdate()
    {
        if(character.GetComponent<Guard>() != false)
        {
            OnCharacterPush();
            OnCharacterShove();
            OnCharacterKick();
            OnWin();
			OnLose();
        }
    }
}
