using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isDead = false;
    protected bool isHit = false;

    protected Player player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        /* check for idle animation to stop movement
            ...Info(0) -> pass in 0 since we only have one animation layer
         */
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        if (isDead) return;

        Movement();
    }

    public virtual void Movement() /* this allows subclasses the option to implement this function */
    {
        /* check if enemy sprite needs to flip */
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        /* check if enemy is at a waypoint so it can then idle */
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        /* check if enemy was hit by player and then change animation if needed */
        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }
        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && anim.GetBool("InCombat"))
        {
            /* face right */
            sprite.flipX = false;
        }
        else if (direction.x < 0 && anim.GetBool("InCombat"))
        {
            /* face left */
            sprite.flipX = true;
        }

        /* check if player is out of aggro range */
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
    }
}
