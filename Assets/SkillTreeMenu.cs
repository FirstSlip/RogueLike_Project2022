using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeMenu : MonoBehaviour
{
    private SkillTree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.Find("Canvas").GetComponent<SkillTree>();
    }

    // Update is called once per frame
    void Update()
    {
        Text[] text = gameObject.GetComponentsInChildren<Text>();

        text[1].text = tree.dmgAmp.ToString() + "%";
        text[2].text = tree.intelligence.ToString();

        text[4].text = tree.healthPool.ToString();
        text[5].text = tree.strength.ToString();

        text[7].text = tree.reducedDashCD.ToString() + "%";
        text[8].text = tree.dexterity.ToString();
    }
}
