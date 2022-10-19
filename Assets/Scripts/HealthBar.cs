using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image HealthBarImg;
    //public GameObject hero;
    private int maxHP;
    [HideInInspector]
    private int currentHP;
    private float prevFill;
    public Image backgImg;

    void Start()
    {
        maxHP = Character.Health;
        currentHP = Character.currentHealth;
        HealthBarImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHP = Character.Health;
        currentHP = Character.currentHealth;
        prevFill = HealthBarImg.fillAmount;
        HealthBarImg.fillAmount = (float)currentHP / maxHP;
        //Debug.Log(prevFill + " aaa " + HealthBarImg.fillAmount);
        if (prevFill != HealthBarImg.fillAmount)
        {
            //Debug.Log("aaa");
            StartCoroutine(ChangeFill(prevFill, HealthBarImg.fillAmount));
        }
    }

    private IEnumerator ChangeFill(float preFill, float fillAmount)
    {
        yield return new WaitForSeconds(0.5f);
        float fil = Math.Abs(preFill - fillAmount);
        for (int i = 0; i < 100; i++)
        {
            backgImg.fillAmount = fillAmount + (99-i) * fil / 100;
            yield return new WaitForSeconds(0.005f);
        }
        
    }
}
