using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private bool attacking;
    private Rigidbody2D rb;
    private Vector2 moveEnemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = player.transform.position - transform.position;
        if (move.sqrMagnitude < 2.2f && !attacking)
        {
            moveEnemy = Vector2.zero;
            StartCoroutine(WaitForEndOfAttackAnimation());
        }
        else if (!attacking && !anim.GetBool("Dead"))
        {
            moveEnemy = new Vector2(move.x, move.y);
            
        }
        else
        {
            moveEnemy = Vector2.zero;
        }
        if (!anim.GetBool("Dead"))
        {
            if (move.x < 0)
                transform.localScale = new Vector3(-3, 3, 1);
            else
                transform.localScale = new Vector3(3, 3, 1);
        }


    }
    //void Update()
    //{
    //    Vector3 move = player.transform.position - transform.position;
    //    if (move.sqrMagnitude < 2.3f && move.y <= 1 && move.y >= -1 && !attacking)
    //    {
    //        moveEnemy = Vector2.zero;
    //        StartCoroutine(WaitForEndOfAttackAnimation());
    //    }
    //    else if (!attacking && !anim.GetBool("Dead"))
    //    {
    //        moveEnemy = new Vector2(move.x, move.y);

    //    }
    //    else
    //    {
    //        moveEnemy = Vector2.zero;
    //    }
    //    if (move.y <= -1 || move.y >= 1)
    //    {
    //        moveEnemy = new Vector2(0, move.y);
    //    }
    //    if ((move.y <= -1 || move.y >= 1) && move.x <= 1.5f && move.x >= -1.5f)
    //    {
    //        if (moveEnemy.x > 0)
    //        {
    //            moveEnemy = new Vector2(3, 0);
    //        }
    //        else
    //        {
    //            moveEnemy = new Vector2(-3, 0);
    //        }
    //    }
    //    if (!anim.GetBool("Dead"))
    //    {
    //        if (move.x < 0)
    //            transform.localScale = new Vector3(-3, 3, 1);
    //        else
    //            transform.localScale = new Vector3(3, 3, 1);
    //    }


    //}
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveEnemy * Time.fixedDeltaTime);
    }

    private IEnumerator WaitForEndOfAttackAnimation()
    {
        anim.SetBool("Attack", true);
        attacking = true;
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(1.1f);
        
        attacking = false;
    }
}
