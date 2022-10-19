using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCrystal : MonoBehaviour
{
    private GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MoveCrystal());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveCrystal()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(10f, 25f)));
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        for (int i = 100; i > 0; i--)
        {
            transform.Translate((hero.transform.position - transform.position) / i);
            yield return new WaitForSeconds(0.005f);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character.currentEXP += 1;
            Destroy(gameObject);
        }
    }
}
