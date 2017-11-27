using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Enemy 
{
    Animator anim;

    //This will be used to change collider size during push animation
    //Vector2 normColliderSize;
    //Vector2 normOffSet;
    //Vector2 colliderSizeEnemy;
    //Vector2 colliderOffSetEnemy;

    void Awake()
    {
        shoveAir = 1f;
        shoveDist.Set(-2.5f, 0f, 0f);

        /*
        doestnt work

        normColliderSize = enemy.GetComponent<BoxCollider2D>().size;
        normOffSet = enemy.GetComponent<BoxCollider2D>().offset;
        colliderSizeEnemy = new Vector2(0.5801693f, 0.8001462f);
        colliderOffSetEnemy = new Vector2(0.1323348f, -0.004211366f);
        */
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

    void FixedUpdate()
    {
        anim.SetBool("Grapple", p.grapple);

        if (p.grapple && p.kickCoroutine == null && p.shoveCoroutine == null && p.chargeCoroutine == null && !p.pushing)
        {
            //Debug.Log("Resisting..");
            Resist();

            //Doesnt work
            //enemy.GetComponent<BoxCollider2D>().size = colliderSizeEnemy;
            //enemy.GetComponent<BoxCollider2D>().offset = colliderOffSetEnemy;
        }
        else
        {
            resisting = false;

            //Doesnt work
            //enemy.GetComponent<BoxCollider2D>().size = normOffSet;
            //enemy.GetComponent<BoxCollider2D>().size = normColliderSize;
        }
        Debug.Log("resisting = " + resisting);

        anim.SetBool("Resist", resisting);
    }
}
