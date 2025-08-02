using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : MonoBehaviour
{
    public string abilityName;
    public string description;
    public int currentLevel;
    public int maxLevel;

    public AbilityData(string abilityName, string description, int maxLevel)
    {
        this.abilityName = abilityName;
        this.description = description;
        this.currentLevel = 0;
        this.maxLevel = maxLevel;
    }
    public AbilityData(string abilityName, string description)
    {
        this.abilityName = abilityName;
        this.description = description;
        this.currentLevel = 0;
        this.maxLevel = 0;
    }
}
