using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Enemy 
{
    Animator anim;

    void Awake()
    {
        shoveAir = 1f;
        shoveDist.Set(-2.5f, 0f, 0f);

        //sets animation component
        anim = this.GetComponent<Animator>();
    }

    void OnEnable()
    {
        ActionAnimationHandler.onPush += this.OnCharacterPush;
        ActionAnimationHandler.onShove += this.OnCharacterShove;
        ActionAnimationHandler.onKick += this.OnCharacterKick;
    }

    void OnDisable()
    {
        ActionAnimationHandler.onPush -= this.OnCharacterPush;
        ActionAnimationHandler.onShove -= this.OnCharacterShove;
        ActionAnimationHandler.onKick -= this.OnCharacterKick;
    }

    public void OnCharacterPush()
    {
        anim.SetBool("Pushed", p.pushing);
    }

    public void OnCharacterShove()
    {
        anim.SetBool("Shoved", p.shoving);
    }

    public void OnCharacterKick()
    {
        anim.SetBool("Kicked", p.kicking);
    }
    private void Start()
    {
        groundY = this.gameObject.transform.position.y;
    }
    void FixedUpdate()
    {
        anim.SetBool("Grapple", p.grapple);
        if(this.gameObject.transform.position.y == groundY)
        {
            canBreak = true;
        }
        if (p.grapple && p.kickCoroutine == null && p.shoveCoroutine == null && p.chargeCoroutine == null && !p.pushing)
        {
            //Debug.Log("Resisting..");
            Resist();
        }
        else
        {
            resisting = false;
        }
        //Debug.Log("resisting = " + resisting);

        anim.SetBool("Resist", resisting);
    }
}
