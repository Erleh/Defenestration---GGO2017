using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObstacle : MonoBehaviour
{
    public FatigueController fc;
    public GameObject player;
    public Player p;
    public Vector3 extendDist;
    public bool extend;
    public bool onCeiling;
    public float fatigueRelief;
    //public float air;
    private void Awake()
    {
        p = player.GetComponent<Player>();
    }
    // Use this for initialization
    //if another object (enemy) enters this object's detection radius, trigger the event
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Entered object space.");
        //specify enemy entrance
        if(col.gameObject.CompareTag("Enemy"))
        {
            //if enemy is getting shoved through this radius
            if ((p.shoving && !onCeiling) || (p.kicking && onCeiling))
            {
                //Debug.Log("Loc: " + enemy.transform.position + "\n" + "Extend: " + extendDist);
                fc.AddFatigue(-fatigueRelief);
                //Debug.Log(extendDist);
               // Destroy(this.gameObject);
            }

        }

        //Relieve some player fatigue
    }
    public Vector3 getExtension() { return extendDist; }
    // Update is called once per frame
    void Update ()
    {

    }
}
