using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameLoader : MonoBehaviour {

    //sounds
    public AudioSource buttonPress;
    public AudioSource startGame;
    public CanvasGroup canvasgroup;
    bool ispressed = false;

    //references menusoundscript
    public MenuSoundScript menuSound;

    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }

    //fades scene into the next
    public IEnumerator FadeOut()
    {
        startGame.Play();
        canvasgroup = GetComponent<CanvasGroup>();
        while (canvasgroup.alpha > 0)
        {
            menuSound.playedCurrently.volume -= Time.deltaTime;
            canvasgroup.alpha -= Time.deltaTime;
            yield return null;
        }
        canvasgroup.interactable = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !ispressed)
        {
            buttonPress.Play();
            StartCoroutine(FadeOut());
            StartCoroutine(LoadGame());
            ispressed = true;
            //Debug.Log("gets here");
            //SceneManager.LoadScene("GameScene");
        }
	}
}
