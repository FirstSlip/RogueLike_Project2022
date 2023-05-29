using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private float lifetime = 0f;
    private ISpell spell;
    public bool isStopped = false;
    
    private bool isHited = false;
    // Start is called before the first frame update
    void Start()
    {
        //isStopped = false;
        spell = gameObject.GetComponent<ISpell>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null && !isHited)
        {
            isHited = true;
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            }
            isStopped = true;
            spell.OnImpact(hitInfo);
        }
        if (!isStopped) transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (lifetime >= 5f && !isHited)
        {
            isHited = true;
            isStopped = true;
            spell.OnImpact(hitInfo);
        }
        lifetime += Time.deltaTime;
    }

    //private IEnumerator AddFireExplode(int id, RaycastHit2D hit)
    //{
    //    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //    minifire[id] = Instantiate(Resources.Load("Prefabs/miniFire") as GameObject, hit.point, Quaternion.Euler(0, 0, 0));
    //    minifire[id].GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(5f, 20f)));
    //    yield return new WaitForSeconds(1);
    //    Destroy(minifire[id]);
    //    yield return new WaitForSeconds(0.2f);
    //    Destroy(gameObject);
    //}
}
