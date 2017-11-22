using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayable
{
	void OnEnable();
	void OnDisable();

	void Push(GameObject g);
	void Shove(GameObject g);
	void Kick(GameObject g);
}