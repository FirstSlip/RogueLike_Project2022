using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingProjectile : MonoBehaviour
{
    private Vector2 direction;
    public Vector2 startDirection;
    private float speed;
    private int damage;
    private bool isActive = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 8;
        damage = 100;
        isActive = false;
        StartCoroutine(WaitForStartChasing());
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>());
        rb.AddForce(transform.right * speed * 35);
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0 && isActive)
        {
            var nearestEnemy = enemies[0];
            var distance = 100f;
            foreach (var enemy in enemies)
                if (Vector2.Distance(enemy.transform.position, transform.position) < distance)
                {
                    nearestEnemy = enemy;
                    distance = Vector2.Distance(enemy.transform.position, transform.position);
                }
            Vector2 preDirection = nearestEnemy.transform.position - transform.position;
            var modifier = preDirection.magnitude / speed;
            direction = preDirection / modifier;
            Debug.Log("Chasing_Projectile.direction = " + "( " + preDirection.magnitude + " "  + speed + " ) " + modifier + " " + direction);
        }
        else
            direction = Vector2.zero;
        //Debug.Log("Chasing_Projectile.direction = " + direction);
        rb.AddForce(direction);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().TakeDamage(damage);
        else 
            if (collision.gameObject.layer == LayerMask.NameToLayer("Solid"))
                Destroy(gameObject);
    }

    private IEnumerator WaitForStartChasing()
    {
        yield return new WaitForSeconds(0.6f);
        isActive = true;
        StartCoroutine(WaitForDestroy());
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5);
    }
}
