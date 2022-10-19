using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private bool attacking;
    private Rigidbody2D rb;
    private Vector2 moveEnemy;
    private Transform center;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        center = GetComponentsInChildren<Transform>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = player.transform.position - center.position;
        if (move.magnitude < 4f && !attacking && Mathf.Abs(move.y) < 2)
        {
            moveEnemy = Vector2.zero;
            StartCoroutine(WaitForEndOfAttackAnimation());
        }
        else if (!attacking && !anim.GetBool("Dead") && !anim.GetBool("Spell"))
        {
            moveEnemy = new Vector2(move.x, move.y);

        }
        else
        {
            moveEnemy = Vector2.zero;
        }
        if (!anim.GetBool("Dead") || !anim.GetBool("Spell"))
        {
            if (move.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        if (anim.GetBool("Dead"))
        {
            BossTrigger.bossIsDead = true;
        }

    }
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
