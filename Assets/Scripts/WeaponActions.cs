using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponActions : MonoBehaviour
{
    public GameObject fireball;
    public GameObject waterBall;
    public GameObject bane;
    public GameObject laser;
    public Transform shotPoint;
    public int idOfSkillSocket = 1;
    private bool isChangingSkill;
    public LayerMask whatIsSolid;
    private bool laserIsActive = false;
    private Animator gun;
    public Animator player;
    public GameObject inventory;
    public Image laserIcon;
    public Sprite arms0;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Animator>();
        //gun.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.haveLaser)
            laserIcon.enabled = true;
        else
            laserIcon.enabled = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            idOfSkillSocket = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            idOfSkillSocket = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            idOfSkillSocket = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            idOfSkillSocket = 4;
        SkillBar.ChoosenSkill = idOfSkillSocket;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotZ > -45 && rotZ < 45)
        {
            gun.GetComponent<SpriteRenderer>().flipY = false;
            gun.GetComponent<SpriteRenderer>().flipX = false;
            gun.GetComponent<SpriteRenderer>().sortingOrder = 11;
            if (player.GetFloat("Speed") < 0.1)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
            }
            gun.SetFloat("Horizontal", 1);
            gun.SetFloat("Vertical", 0);
            player.SetFloat("HorizontalStop", 1);
            player.SetFloat("VerticalStop", 0);
        }
        if (rotZ >= 45 && rotZ <= 135)
        {
            gun.GetComponent<SpriteRenderer>().flipY = true;
            gun.GetComponent<SpriteRenderer>().flipX = true;
            gun.GetComponent<SpriteRenderer>().sortingOrder = 9;
            if (player.GetFloat("Speed") < 0.1)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
            }
            gun.SetFloat("Horizontal", 0);
            gun.SetFloat("Vertical", 1);
            player.SetFloat("HorizontalStop", 0);
            player.SetFloat("VerticalStop", 1);
        }
        if (rotZ >= -135 && rotZ <= -45)
        {
            gun.GetComponent<SpriteRenderer>().flipY = false;
            gun.GetComponent<SpriteRenderer>().flipX = false;
            gun.GetComponent<SpriteRenderer>().sortingOrder = 11;
            if (player.GetFloat("Speed") < 0.1)
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
            }
            gun.SetFloat("Horizontal", 0);
            gun.SetFloat("Vertical", -1);
            player.SetFloat("HorizontalStop", 0);
            player.SetFloat("VerticalStop", -1);
        }
        if (rotZ > 135 || rotZ < -135)
        {
            gun.GetComponent<SpriteRenderer>().flipY = true;
            gun.GetComponent<SpriteRenderer>().flipX = true;
            gun.GetComponent<SpriteRenderer>().sortingOrder = 11;
            if (player.GetFloat("Speed") < 0.1)
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
            }
            gun.SetFloat("Horizontal", -1);
            gun.SetFloat("Vertical", 0);
            player.SetFloat("VerticalStop", 0);
            player.SetFloat("HorizontalStop", -1);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        switch (idOfSkillSocket)
        {
            case 1:
                if (SkillBar.GetSkillsCD()[idOfSkillSocket - 1] <= 0)
                {
                    if (Input.GetMouseButton(0) && inventory.activeSelf == false)
                    {
                        SkillBar.GetSkillsCD()[idOfSkillSocket - 1] = SkillBar.GetStartSkillsCD()[idOfSkillSocket - 1];
                        StartCoroutine(Attack(fireball));
                    }
                }
                break;
            case 2:
                if (SkillBar.GetSkillsCD()[idOfSkillSocket - 1] <= 0)
                {
                    if (Input.GetMouseButton(0) && inventory.activeSelf == false)
                    {
                        SkillBar.GetSkillsCD()[idOfSkillSocket - 1] = SkillBar.GetStartSkillsCD()[idOfSkillSocket - 1];
                        StartCoroutine(CastWaterBall(waterBall, difference));
                    }
                }
                break;
            case 3:
                if (SkillBar.GetSkillsCD()[idOfSkillSocket - 1] <= 0)
                {
                    if (Input.GetMouseButton(0) && inventory.activeSelf == false)
                    {
                        SkillBar.GetSkillsCD()[idOfSkillSocket - 1] = SkillBar.GetStartSkillsCD()[idOfSkillSocket - 1];
                        StartCoroutine(TripleAttack(fireball));
                    }
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator Attack(GameObject spell)
    {
        Character.attack = true;
        //gun.enabled = true;
        gun.SetBool("IsAttack", true);
        player.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.1f);
        gun.SetBool("IsAttack", false);
        yield return new WaitForSeconds(0.2f);
        Instantiate(spell, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        //gun.enabled = false;
        player.SetBool("IsAttack", false);
        Character.attack = false;
    }
    private IEnumerator CastWaterBall(GameObject spell, Vector3 difference)
    {
        Character.attack = true;

        var waterball = Instantiate(spell, shotPoint.position, transform.rotation);
        if (difference.y > 0) waterBall.GetComponent<SpriteRenderer>().sortingOrder = 7;
        else waterBall.GetComponent<SpriteRenderer>().sortingOrder = 5;

        waterball.GetComponent<Projectile>().isStopped = true;
        waterball.GetComponent<Animator>().Play("WaterBallCast");
        yield return new WaitForSeconds(0.2f);
        waterball.GetComponent<Projectile>().isStopped = false;
        yield return new WaitForSeconds(0.05f);
        waterball.GetComponent<Animator>().Play("WaterBallDefault");

        //gun.enabled = true;
        gun.SetBool("IsAttack", true);
        player.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.2f);
        gun.SetBool("IsAttack", false);
        
        
        yield return new WaitForSeconds(0.2f);
        //gun.enabled = false;
        player.SetBool("IsAttack", false);
        Character.attack = false;
    }
    private IEnumerator TripleAttack(GameObject spell)
    {
        Character.attack = true;
        //gun.enabled = true;
        gun.SetBool("IsAttack", true);
        player.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.1f);
        gun.SetBool("IsAttack", false);
        yield return new WaitForSeconds(0.2f);
        Debug.Log(transform.rotation.eulerAngles);
        Instantiate(spell, shotPoint.position, transform.rotation);
        Instantiate(spell, shotPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 15));
        Instantiate(spell, shotPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 15));
        yield return new WaitForSeconds(0.2f);
        //gun.enabled = false;
        player.SetBool("IsAttack", false);
        Character.attack = false;
    }
}
