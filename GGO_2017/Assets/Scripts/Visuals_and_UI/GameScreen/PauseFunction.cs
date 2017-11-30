using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseFunction : MonoBehaviour {

    public Image pauseScreen;
    // Use this for initialization
    private void Awake()
    {
        GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
    }
    void Start ()
    {
        pauseScreen = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
	}
    void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            pauseScreen.enabled = false;
            Time.timeScale = 1;
            
        }
        else
        {
            pauseScreen.enabled = true;
            Time.timeScale = 0;
        }
    }
}
