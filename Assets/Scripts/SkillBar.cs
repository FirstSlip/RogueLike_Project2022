using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    //private float skillCD1;
    //private float skillCD2;
    //private float skillCD3;
    //private float skillCD4;
    public static int ChoosenSkill = 1;
    public int skillsCount;
    private static float[] skillsCD;

    //private float startSkillCD1;
    //private float startSkillCD2;
    //private float startSkillCD3;
    //private float startSkillCD4;
    private static float[] startSkillsCD;
    //public Image skill1;
    //public Image skill2;
    public Image[] skill = new Image[4];
    public GameObject[] skillChoice = new GameObject[4];

    public static float[] GetSkillsCD()
    {
        return skillsCD;
    }

    public static float[] GetStartSkillsCD()
    {
        return startSkillsCD;
    }

    // Start is called before the first frame update
    void Start()
    {
        skillsCD = new float[] { 0, 0, 0, 0 };
        startSkillsCD = new float[] { 1, 1, 1, 4 };
        for (int i = 0; i < 4; i++)
        {
            skillChoice[i].GetComponent<Image>().enabled = false;
        }
        
        //startSkillCD1 = 1;
        //startSkillCD1 = 4;
        //startSkillCD1 = 4;
        //startSkillCD1 = 4;

        //skillCD1 = 0;
        //skillCD2 = 0;
        //skillCD3 = 0;
        //skillCD4 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skillsCD.Length; i++)
        {
            if (skillsCD[i] > 0)
            {
                skillsCD[i] -= Time.deltaTime;
                skill[i].fillAmount = skillsCD[i] / startSkillsCD[i];
            }
        }
        
        for (int i = 0; i < 4; i++)
        {
            if (ChoosenSkill-1 != i)
                skillChoice[i].GetComponent<Image>().enabled = false;
            else
                skillChoice[ChoosenSkill-1].GetComponent<Image>().enabled = true;
        }
    }
}
