using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Player
{
    //Animator
    public float passiveF;
    public float shoveF;
    public float kickF;
    public float strK;
    public float strS;
    public GameplayHandler gph;
    //Sets initial values
    void Awake()
    {
        PassiveFatigue = passiveF;
        PushFatigue = 0.025f;
        ShoveFatigue = shoveF;
        KickFatigue = kickF;
        //speed = -.2f;
        StrOfKick = strK;
        StrOfShove = strS;
        /*Need implementation first*/
        //ShoveFatigue = 5f;
        //KickFatigue = 7f;

        anim = this.GetComponent<Animator>();
    }

    void OnEnable()
    {
        ActionAnimationHandler.onPush += this.OnCharacterPush;
        ActionAnimationHandler.onShove += this.OnCharacterShove;
        ActionAnimationHandler.onKick += this.OnCharacterKick;
        ActionAnimationHandler.onWin += this.OnWin;
		ActionAnimationHandler.onLose += this.OnLose;
    }

    void OnDisable()
    {
        ActionAnimationHandler.onPush -= this.OnCharacterPush;
        ActionAnimationHandler.onShove -= this.OnCharacterShove;
        ActionAnimationHandler.onKick -= this.OnCharacterKick;
        ActionAnimationHandler.onWin -= this.OnWin;
		ActionAnimationHandler.onLose -= this.OnLose;
    }

    void Start()
    {
        anim.SetBool("ChargeAt", charging);
    }

    public void OnCharacterPush()
    {
        anim.SetBool("Push", pushing);
    }

    public void OnCharacterShove()
    {
        anim.SetBool("Shove", shoving);
    }

    public void OnCharacterKick()
    {
        anim.SetBool("Kick", kicking);
    }

    public void OnWin()
    {
        //Debug.Log("defenestrated");
        if(gph.win)
        {
            //Debug.Log("gets here");
            if(enemy != null)
            {
                //Debug.Log("delete bob");
                Destroy(enemy);
            }
        }
        anim.SetBool("Defenestrate", win);
    }

	public void OnLose()
	{
		anim.SetBool("Lose", lose);
	}

    public AudioSource failJingle;
    public GameObject defeatSprite;
    public InGameMusic curr;
    public void DefeatPose()
    {
        curr.GetCurrMusic().enabled = false;

        failJingle.Play();
        defeatSprite.GetComponent<SpriteRenderer>().enabled = true;
    }

	//Specific for animation timing of braking glass
	public bool defenestrated;
	public void DefenistratedBob()
	{
		defenestrated = true;
	}

	//Specific for victory sprite during animation
	public GameObject victorySprite;
    public GameObject cheerVictory;
    public AudioSource victoryJingle;
    
	public void VictoryPose()
	{
        curr.GetCurrMusic().enabled = false;
        victoryJingle.Play();

        victorySprite.GetComponent<SpriteRenderer>().enabled = true;

        StartCoroutine(Cheer());
        //cheerVictory.GetComponent<SpriteRenderer>().enabled = true;
        //cheerVictory.GetComponent<Animator>().enabled = true;
	}

    public IEnumerator Cheer()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("Cheer", true);

        cheerVictory.GetComponent<SpriteRenderer>().enabled = true;
        cheerVictory.GetComponent<Animator>().enabled = true;
    }
    void FixedUpdate()
    {
    }
}
