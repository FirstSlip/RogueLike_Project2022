using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public int intelligence;
    public int strength;
    public int dexterity;
    public int dmgAmp = 0;
    public int healthPool = 0;
    public int reducedDashCD = 0;
    // Start is called before the first frame update
    void Awake()
    {
        SaveSerial.LoadGame();
        if (SaveSerial.playerStats.ContainsKey("intelligence"))
            intelligence = SaveSerial.playerStats["intelligence"];
        else
        {
            SaveSerial.playerStats.Add("intelligence", 0);
            intelligence = 0;
        }
        if (SaveSerial.playerStats.ContainsKey("strength"))
            strength = SaveSerial.playerStats["strength"];
        else
        {
            SaveSerial.playerStats.Add("strength", 0);
            strength = 0;
        }
        if (SaveSerial.playerStats.ContainsKey("dexterity"))
            dexterity = SaveSerial.playerStats["dexterity"];
        else
        {
            SaveSerial.playerStats.Add("dexterity", 0);
            dexterity = 0;
        }
        dmgAmp = intelligence * 10;
        healthPool = strength * 20;
        reducedDashCD = dexterity * 5;
        //Character.Health = 100 + healthPool;
        //Character.currentHealth = Character.Health;
        Debug.Log("Loaded Stats: " + intelligence + " " + strength + " " + dexterity);
    }
    

    // Update is called once per frame
    void Update()
    {
        dmgAmp = intelligence * 10;
        healthPool = strength * 20;
        reducedDashCD = dexterity * 5;
    }

    public void LevelUpInt()
    {
        if (Character.currentEXP >= Character.EXPToLevel)
        {
            intelligence += 1;
            SaveSerial.playerStats["intelligence"] = intelligence;
            SaveSerial.SaveGame();
            Character.currentEXP -= Character.EXPToLevel;
        }
    }
    public void LevelUpStr()
    {
        if (Character.currentEXP >= Character.EXPToLevel)
        {
            strength += 1;
            SaveSerial.playerStats["strength"] = strength;
            SaveSerial.SaveGame();
            Character.currentEXP -= Character.EXPToLevel;
        }
    }
    public void LevelUpDex()
    {
        if (Character.currentEXP >= Character.EXPToLevel)
        {
            dexterity += 1;
            SaveSerial.playerStats["dexterity"] = dexterity;
            SaveSerial.SaveGame();
            Character.currentEXP -= Character.EXPToLevel;
        }
    }

    public void ResetStats()
    {
        SaveSerial.ResetData();
        intelligence = 0;
        strength = 0;
        dexterity = 0;
        if (Character.currentHealth > Character.Health)
        {
            Character.currentHealth = Character.Health;
        }
    }

}
