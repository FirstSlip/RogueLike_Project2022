using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDamage : MonoBehaviour
{
    public float attackRange;
    public int damage = 20;
    public Transform attackPos;
    public LayerMask hero;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteSpell());
    }

    private IEnumerator DeleteSpell()
    {
        yield return new WaitForSeconds(1.1f);
        GameObject.FindGameObjectWithTag("HitScreen").GetComponent<Image>().enabled = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {

        Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, attackRange, hero);
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().GetInvulnerable() && player.Length != 0)
        {
            player[0].GetComponent<Character>().TakeDamage(damage);
            StartCoroutine(EnableHitScreen());
        }

    }
    private IEnumerator EnableHitScreen()
    {
        GameObject.FindGameObjectWithTag("HitScreen").GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("HitScreen").GetComponent<Image>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
