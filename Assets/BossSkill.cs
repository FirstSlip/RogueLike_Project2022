using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private float timer = 10f;
    private Animator boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            StartCoroutine(Spell());
        }
    }
    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
    }

    private IEnumerator Spell()
    {
        timer = 10f;
        boss.SetBool("Spell", true);
        yield return new WaitForSeconds(1);
        boss.SetBool("Spell", false);
        Instantiate(Resources.Load("Prefabs/BossSkill") as GameObject, 
            new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, 
            GameObject.FindGameObjectWithTag("Player").transform.position.y + 2),
            Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1);
        Instantiate(Resources.Load("Prefabs/BossSkill") as GameObject,
            new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y + 2),
            Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1);
        Instantiate(Resources.Load("Prefabs/BossSkill") as GameObject,
            new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y + 2),
            Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1.1f);
    }
}
