using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; } /* auto property */

    /* Use for initialization */
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        //do nothing
    }

    public void Damage()
    {
        if (isDead) return;
        Health--;
        if(Health < 1)
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

    public override void Movement()
    {
        
    }

    public void Attack()
    {
        Instantiate(
            acidEffectPrefab,
            transform.position,
            Quaternion.identity
            );
    }
}
