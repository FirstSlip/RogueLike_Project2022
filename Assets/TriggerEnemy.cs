using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public GameObject[] spawners;
    private GameObject[] borders;
    private bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        borders = GameObject.FindGameObjectsWithTag("Border");
        foreach (var e in borders)
        {
            e.GetComponent<SpriteRenderer>().enabled = false;
            e.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            foreach (var e in borders)
            {
                e.GetComponent<SpriteRenderer>().enabled = false;
                e.GetComponent<BoxCollider2D>().enabled = false;
            }
            spawn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        borders = GameObject.FindGameObjectsWithTag("Border");
        if (collision.tag == "Player")
        {
            foreach(var e in borders)
            {
                e.GetComponent<SpriteRenderer>().enabled = true;
                e.GetComponent<BoxCollider2D>().enabled = true;
            }
            foreach(var e in spawners)
            {
                e.SetActive(true);
            }
            StartCoroutine(Waiter());
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(3f);
        spawn = true;
    }
}
