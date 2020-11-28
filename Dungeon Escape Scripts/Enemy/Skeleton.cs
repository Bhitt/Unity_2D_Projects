using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; } /* auto property */

    /* Use for initialization */
    public override void Init()
    {
        base.Init();
        Health = base.health; /* grab value from inspector which comes from base class */
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        if (isDead) return;
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(
                diamondPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject; /* cast the diamond that is instantiated as a gameobject */
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

}
