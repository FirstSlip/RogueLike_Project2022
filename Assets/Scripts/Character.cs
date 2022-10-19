using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public static int Health = 100;
    public static int currentHealth;
    public static int Mana;
    public bool haveShift = true;
    public static bool attack = false;
    private bool isInvulnerable = false;
    public static int currentEXP = 0;
    public static int EXPToLevel = 10;
    public Image ExpCrystal;
    private bool onLevelup = false;
    /*public List<Status> status = new List<Status>();
    public List<Image> statusImages;*/
    private float SelfDamageMultiplier;
    public GameObject levelUpMenu;
    public GameObject levelUpText;
    public SkillTree sTree;
    public static bool haveLaser;
    public Image blackScreen;
    public Text died;

    // Start is called before the first frame update
    void Start()
    {
        haveLaser = false;
        Health = 100 + sTree.healthPool;
        currentHealth = Health;
        levelUpMenu.SetActive(false);
        levelUpText.SetActive(false);
        died.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("HitScreen").GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Health = 100 + sTree.healthPool;
        Debug.Log(Health + " " + currentHealth);
        if (currentHealth <= 0)
            StartCoroutine(PlayerDead());
        ExpCrystal.fillAmount = (float)currentEXP / (float)EXPToLevel;
        if (currentEXP >= EXPToLevel && !onLevelup)
            StartCoroutine(LevelUP());

        if (onLevelup)
        {
            levelUpText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.L))
            {
                levelUpMenu.SetActive(true);
            }
        }
        else
        {
            levelUpText.SetActive(false);
        }
        if (currentEXP < EXPToLevel)
        {
            StopCoroutine(LevelUP());
            onLevelup = false;
            ExpCrystal.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    private IEnumerator PlayerDead()
    {
        foreach (var l in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            l.GetComponent<Animator>().enabled = false;
        }
        foreach (var l in GameObject.FindGameObjectsWithTag("Border"))
        {
            l.GetComponent<Animator>().enabled = false;
        }
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.GetComponent<WeaponActions>().enabled = false;
        for (float i = 0; i < 1f; i+=0.01f)
        {
            blackScreen.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
            Debug.Log(i);
        }
        died.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    private IEnumerator LevelUP()
    {
        onLevelup = true;
        while (currentEXP >= EXPToLevel)
        {
            for (float i = 1.01f; i <= 1.15f; i += 0.01f)
            {
                ExpCrystal.transform.localScale = new Vector3(i, i, 1);
                yield return new WaitForSeconds(0.05f);
            }
            for (float i = 1.15f; i >= 1f; i -= 0.01f)
            {
                ExpCrystal.transform.localScale = new Vector3(i, i, 1);
                yield return new WaitForSeconds(0.05f);
            }

        }
    }
    public bool GetInvulnerable()
    {
        return isInvulnerable;
    }

    public void SetInvulnerable(bool inv)
    {
        isInvulnerable = inv;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            StartCoroutine(DamageInv());
        }
    }

    private IEnumerator DamageInv()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }
}
