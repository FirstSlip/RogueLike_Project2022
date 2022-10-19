using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //private string reward;
    public GameObject reward;
    public Sprite openChest;
    private GameObject hero;
    private bool isNear;
    private bool isOpened = false;
    private ItemDataBase item;
    // Start is called before the first frame update
    void Start()
    {
        //reward = "Coins";
        hero = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        item = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ItemDataBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(hero.transform.position.x - gameObject.transform.position.x) <= 1.5f &&
            Math.Abs(hero.transform.position.y - gameObject.transform.position.y) <= 1.5f && !isOpened)
        {
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            isNear = true;
        }
        else
        {
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            isNear = false;
        }
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = true;
            StartCoroutine(ThrowReward());
        }
    }

    private IEnumerator ThrowReward()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openChest;
        GameObject itemReward = item.CreateItem(transform.position);
        itemReward.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), 15));
        GameObject rew = Instantiate(reward, new Vector3(transform.position.x, transform.position.y + 1f, -0.2f), transform.rotation);
        rew.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), 15));
        yield return new WaitForSeconds(2.1f);
        if (rew != null)
            rew.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (itemReward != null)
            itemReward.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }
}