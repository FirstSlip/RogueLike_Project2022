using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int hp = 160;
    public int exp = 2;
    public int damage = 10;
    private Animator anim;
    private bool dead;
    public Transform attackPos;
    public float attackRange;
    public LayerMask hero;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            StartCoroutine(ColorRed());
        }
        if (hp <= 0 && !dead)
            StartCoroutine(DestroyEnemy());
    }

    private IEnumerator ColorRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator DestroyEnemy()
    {
        foreach (var j in GetComponentsInChildren<Collider2D>())
            j.enabled = false;
        dead = true;
        anim.SetBool("Dead", true);
        anim.Play("Dead");
        for (int i = 0; i < exp; i++)
        {
            Instantiate(Resources.Load("Prefabs/Exp") as GameObject, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void Hit()
    {

        Collider2D[] player = Physics2D.OverlapCircleAll(attackPos.position, attackRange, hero);
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().GetInvulnerable() && player.Length != 0)
        {
            player[0].GetComponent<Character>().TakeDamage(damage);
            GameObject.FindGameObjectWithTag("HitScreen").GetComponent<HitScreen>().enableHitScreen = true;
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Spells"))
        TakeDamage(1);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
