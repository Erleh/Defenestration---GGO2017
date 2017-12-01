using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameLoader : MonoBehaviour {

    //sounds
    public AudioSource buttonPress;
    public AudioSource startGame;

    public IEnumerator LoadGame()
    {
        startGame.Play();

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameScene");
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonPress.Play();

            StartCoroutine(LoadGame());
            //Debug.Log("gets here");
            //SceneManager.LoadScene("GameScene");
        }
	}
}
