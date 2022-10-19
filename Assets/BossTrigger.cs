using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public static bool bossIsDead = false;
    public GameObject boss;
    public GameObject room;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(false);
        bossIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossIsDead)
        {
            foreach(var obj in GameObject.FindGameObjectsWithTag("Border"))
            {
                Destroy(obj);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            collision.GetComponent<PlayerController>().enabled = false;
            collision.GetComponent<Animator>().SetFloat("Speed", 0);
            collision.GetComponent<Animator>().SetFloat("VerticalStop", 1);
            collision.GetComponentInChildren<WeaponActions>().enabled = false;
            triggered = true;
            StartCoroutine(MoveCamera(collision));
        }
    }

    private IEnumerator MoveCamera(Collider2D collision)
    {
        Vector3 position = room.transform.position - Camera.main.transform.position;
        boss.SetActive(true);
        boss.GetComponent<BossMovement>().enabled = false;
        for (int i = 0; i < 100; i++)
        {
            Camera.main.transform.Translate(new Vector3(position.x / 100, (position.y + 4) / 100, 0));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3f);
        boss.GetComponent<Animator>().SetBool("StartFight", true);
        boss.GetComponent<BossMovement>().enabled = true;
        collision.GetComponent<PlayerController>().enabled = true;
        collision.GetComponentInChildren<WeaponActions>().enabled = true;
    }
}
