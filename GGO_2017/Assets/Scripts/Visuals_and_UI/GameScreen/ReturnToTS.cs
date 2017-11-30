using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToTS : MonoBehaviour {
    public GameObject player;
    public FatigueController fc;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() { }
		/*if(player.GetComponent<Player>().win || fc.loseGame)
        {
            //Debug.Log(player.GetComponent<Player>().win);
            StartCoroutine(CoGameOver());
        }
	}
    public IEnumerator CoGameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("TitleScreen");
    }*/
}
