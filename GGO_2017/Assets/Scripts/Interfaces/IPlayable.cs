using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayable
{
	void OnEnable();
	void OnDisable();

	void OnCharacterPush();
	void OnCharacterShove();
	void OnCharacterKick();
}