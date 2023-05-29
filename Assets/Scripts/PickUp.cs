using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private GameObject inventory;
    private GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 range = hero.transform.position - transform.position;
        if (range.sqrMagnitude <= 4f)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.GetComponent<Inventory>().AddItem(gameObject.GetComponent<ItemInfo>().id);
                Destroy(gameObject);
            }
        }
        else
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;

    }
}
