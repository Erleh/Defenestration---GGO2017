using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObstacle : MonoBehaviour
{
    public FatigueController fc;
    public GameObject player;
    public Player p;
    public Vector3 extendDist;
	public GameObject obj;

    public bool extend;
    public bool onCeiling;
	public bool hit;

	public Color oppac;
	public float colorNum = 1;

	public float speedOfKnockAway;
	public float speedOfSpin;
    public float extStr;
    public float fatigueRelief;

    private float randomFlyHeight;
    private float currFlyHeight = 0;

    private bool recovered = false;
    private bool hitObject = false;
    //public float air;

    public GameObject animator;

    private void Awake()
    {
        p = player.GetComponent<Player>();
    }

    public AudioSource objectHitSound;

    public void HitObjectSound()
    {
        objectHitSound.Play();
    }

    // Use this for initialization
    //if another object (enemy) enters this object's detection radius, trigger the event
    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("Entered object space.");
        //specify enemy entrance
        if(col.gameObject.CompareTag("Enemy"))
        {
			if(p.shoving && !onCeiling && !hitObject)
			{
                //to prevent hitting the same object again in the same shove or kick
                hitObject = true;

                if (!recovered)
                {
                    fc.AddFatigue(-fatigueRelief);
                    recovered = true;
                }

                randomFlyHeight = Random.Range(0, 40);
                //obj.transform.position += new Vector3(0, Random.Range(0, 1), 0);

                hit = true;
			}
            //if enemy is getting shoved through this radius
            if (p.kicking && onCeiling && !hitObject)
            {
                //to prevent hitting the same object
                hitObject = true;

                //Debug.Log("Loc: " + enemy.transform.position + "\n" + "Extend: " + extendDist);

                //Makes recovery only occur once
                if (!recovered)
                {
                    fc.AddFatigue(-fatigueRelief);
                    recovered = true;
                }

                animator.GetComponent<Animator>().enabled = true;
                //Debug.Log(extendDist);
                // Destroy(this.gameObject);
            }
        }

        //Relieve some player fatigue
    }
    public Vector3 getExtension() { return extendDist; }
    // Update is called once per frame
    
	void FixedUpdate ()
    {
		if(hit)
		{
			obj.GetComponent<Rigidbody2D>().MoveRotation(obj.GetComponent<Rigidbody2D>().rotation + speedOfSpin * Time.deltaTime);

            Color oppac = obj.GetComponent<SpriteRenderer>().color;
            oppac.a -= .02f;
            obj.GetComponent<SpriteRenderer>().color = oppac;

			if(colorNum > 0)
			{
				colorNum -= .01f;
			}

			obj.transform.position += Vector3.left * Time.deltaTime * speedOfKnockAway;

            //Debug.Log(obj.transform.position.y);

            if (obj.transform.position.y < obj.transform.position.y + randomFlyHeight)
            {
                //Debug.Log(obj.transform.position.y);
                obj.transform.position += Vector3.up * .02f;
            }

            //Debug.Log(obj.GetComponent<SpriteRenderer>().color.a);

            if (obj.GetComponent<SpriteRenderer>().color.a < 0)
            {
                Destroy(obj);
            }
		}
    }
}
