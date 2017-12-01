using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBreakAnimation : MonoBehaviour 
{
	Animator anim;

	public GameObject character;
	Guard player;

	void Awake()
	{
		anim = this.GetComponent<Animator>();

		if(character.GetComponent<Guard>() != null)
		{
			player = character.GetComponent<Guard>();
		}
	}

	void FixedUpdate()
	{
		if(player.defenestrated)
		{
			anim.enabled = true;
		}
	}
}
