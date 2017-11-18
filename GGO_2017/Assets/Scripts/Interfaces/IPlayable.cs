using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayable
{
	void OnEnable();
	void OnDisable();

	void OnCharacterPush(GameObject character);
	void OnCharacterShove(GameObject character);
	void OnCharacterKick(GameObject character);
}