using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderInteraction : MonoBehaviour
{
    private GameObject inventory;
    private GameObject hero;
    private bool interact;
    public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        interact = false;
        hero = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 range = hero.transform.position - transform.position;
        if (range.sqrMagnitude <= 4f && !interact)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                interact = true;
                dialog.SetActive(true);
            }
        }
        else
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
    }
}
