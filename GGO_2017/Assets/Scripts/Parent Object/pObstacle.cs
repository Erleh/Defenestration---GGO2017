using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObstacle : MonoBehaviour
{

    public GameObject obstacle;
    public FatigueController fc;
    public GameObject enemy;
    public Player p;
    public Vector3 extendDist;

    public bool onCeiling;
    public float fatigueRelief;
    //public float air;

    public Coroutine extendShoveCoroutine;
    public Coroutine extendKickCoroutine;
    // Use this for initialization
    void Start () {
		
	}
    //if another object (enemy) enters this object's detection radius, trigger the event
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Entered object space.");
        //specify enemy entrance
        if(col.gameObject.CompareTag("Enemy"))
        {
            //if enemy is getting shoved through this radius
            if (p.shoving /*|| p.kickCoroutine != null*/ )
            {
                Debug.Log("Checking coroutine");
                //determine obstacle type with boolean, start new coroutine accordingly
                if (!onCeiling)
                {
                    Debug.Log(enemy.transform.position);
                    Debug.Log(extendDist);
                    Debug.Log(enemy.transform.position + extendDist);
                    p.extendShoveCoroutine = StartCoroutine(p.CoSExtend(enemy.transform.position + extendDist, 1));
                    fc.AddFatigue(-fatigueRelief);
                   
                }
                Destroy(obstacle);
            }

        }

        //Relieve some player fatigue
    }
    // Update is called once per frame

    //public IEnumerator coKExtend() { yield return new WaitForEndOfFrame(); }
    void Update () {
		
	}
}
