using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private float shiftCD;
    private Character hero;
    public Image Shift;
    private bool isDashing;
    public Animator animator;
    public Image hpPotion;
    public Inventory inv;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hero = GetComponent<Character>();
        Shift.fillAmount = 0;
        hpPotion.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inv.slots[0].GetComponent<SlotData>().id != 0)
            hpPotion.fillAmount = 0;
        else
            hpPotion.fillAmount = 1;
        if (hpPotion.fillAmount == 0)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (Character.currentHealth < Character.Health)
                {
                    if (Character.currentHealth <= Character.Health - 50)
                    {
                        Character.currentHealth += 50;
                    }
                    else
                        Character.currentHealth = Character.Health;
                    Destroy(inv.slots[0].transform.GetChild(0).gameObject);
                    inv.slots[0].GetComponent<SlotData>().id = 0;
                }
            }
        }
        Vector2 moveInput = Vector2.zero;
        if (!Character.attack)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        if (!isDashing)
            moveVelocity = moveInput.normalized * speed;
        if (Input.GetKeyDown(KeyCode.LeftShift) && hero.haveShift && (moveInput.x != 0 || moveInput.y != 0))
        {
            hero.haveShift = false;
            shiftCD = 2f;
            StartCoroutine(Dash(moveInput));
        }
        if (!hero.haveShift)
        {
            shiftCD -= Time.deltaTime;
            Shift.fillAmount = shiftCD / 2f;
            if (shiftCD <= 0)
            {
                hero.haveShift = true;
                //Shift.fillAmount = 1f;
            }
        }
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private IEnumerator Dash(Vector2 axis)
    {
        animator.SetBool("Dash", true);
        foreach (var en in GameObject.FindGameObjectsWithTag("Enemy"))
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), en.GetComponent<Collider2D>(), true);
        isDashing = true;
        hero.SetInvulnerable(true);
        moveVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.03f);
        moveVelocity = axis.normalized * 30;
        yield return new WaitForSeconds(0.1f);
        moveVelocity = moveVelocity / 10;
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Dash", false);
        hero.SetInvulnerable(false);
        foreach (var en in GameObject.FindGameObjectsWithTag("Enemy"))
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), en.GetComponent<Collider2D>(), false);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        //rb.AddForce(moveVelocity * Time.fixedDeltaTime);
    }
}
