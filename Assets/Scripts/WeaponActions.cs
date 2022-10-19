using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponActions : MonoBehaviour
{
    public GameObject fireball;
    public GameObject bane;
    public GameObject laser;
    public Transform shotPoint;
    public int idOfSkillSocket = 1;
    private bool isChangingSkill;
    public LayerMask whatIsSolid;
    private bool laserIsActive = false;
    private GameObject las;
    private Animator gun;
    public Animator player;
    public GameObject inventory;
    public Image laserIcon;
    public Sprite arms0;
    // Start is called before the first frame update
    void Start()
    {
        las = new GameObject();
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
                laserIsActive = false;
                Destroy(las);
                if (SkillBar.GetSkillsCD()[idOfSkillSocket - 1] <= 0)
                {
                    if (Input.GetMouseButton(0) && inventory.activeSelf == false)
                    {
                        SkillBar.GetSkillsCD()[idOfSkillSocket - 1] = SkillBar.GetStartSkillsCD()[idOfSkillSocket - 1];
                        StartCoroutine(Attack());
                    }
                }
                break;
            case 2:
                if (Input.GetMouseButton(0) && Character.haveLaser)
                {
                    Character.attack = true;
                    if (!laserIsActive)
                    {
                        RaycastHit2D hitInfo = Physics2D.Raycast(shotPoint.position, transform.right, 200f, whatIsSolid);
                        //hitInfo.point += new Vector2(1, 0);
                        las = Instantiate(laser, new Vector2(shotPoint.position.x, shotPoint.position.y) +
                        (hitInfo.point - new Vector2(shotPoint.position.x, shotPoint.position.y)) / 2f, transform.rotation);
                        las.transform.localScale = new Vector3((hitInfo.point - new Vector2(shotPoint.position.x, shotPoint.position.y)).magnitude / 5.76f,
                        5f, 1);
                        laserIsActive = true;
                        if (las.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > 0)
                            las.GetComponent<SpriteRenderer>().sortingOrder = 9;
                        else
                            las.GetComponent<SpriteRenderer>().sortingOrder = 11;
                        StartCoroutine(AttackLaser());
                        if (hitInfo.collider.CompareTag("Enemy"))
                        {
                            hitInfo.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(2);
                        }
                    }
                    else
                    {
                        RaycastHit2D hitInfo = Physics2D.Raycast(shotPoint.position, transform.right, 200f, whatIsSolid);
                        las.transform.position = new Vector2(shotPoint.position.x, shotPoint.position.y) +
                        (hitInfo.point - new Vector2(shotPoint.position.x, shotPoint.position.y)) / 2f;
                        las.transform.rotation = transform.rotation;
                        las.transform.localScale = new Vector3((hitInfo.point - new Vector2(shotPoint.position.x, shotPoint.position.y)).magnitude,
                        5f, 1);
                        if (las.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > 0)
                            las.GetComponent<SpriteRenderer>().sortingOrder = 9;
                        else
                            las.GetComponent<SpriteRenderer>().sortingOrder = 11;
                        if (hitInfo.collider.CompareTag("Enemy"))
                        {
                            hitInfo.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(1);
                        }
                    }
                }
                else
                {
                    gun.gameObject.GetComponent<Animator>().enabled = true;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    laserIsActive = false;
                    Destroy(las);
                    gun.SetBool("IsAttack", false);
                    player.SetBool("IsAttack", false);
                    Character.attack = false;
                    gun.gameObject.GetComponent<Animator>().enabled = true;
                }
                break;
            default:
                break;
        }


    }
    private IEnumerator AttackLaser()
    {
        gun.SetBool("IsAttack", true);
        player.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.3f);
        //gun.gameObject.GetComponent<Animator>().enabled = false;
        //gun.gameObject.GetComponent<SpriteRenderer>().sprite = arms0;
        //gun.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    private IEnumerator Attack()
    {
        Character.attack = true;
        //gun.enabled = true;
        gun.SetBool("IsAttack", true);
        player.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.1f);
        gun.SetBool("IsAttack", false);
        yield return new WaitForSeconds(0.2f);
        Instantiate(fireball, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        //gun.enabled = false;
        player.SetBool("IsAttack", false);
        Character.attack = false;
    }
}
