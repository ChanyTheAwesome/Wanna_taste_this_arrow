using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : MonoBehaviour
{
    public string _abilityName;
    public string _description;
    public int _currentLevel;
    public int _maxLevel;

    public AbilityData(string abilityName, string description, int maxLevel)
    {
        _abilityName = abilityName;
        _description = description;
        _currentLevel = 0;
        _maxLevel = maxLevel;
    }
    public AbilityData(string abilityName, string description)
    {
        _abilityName = abilityName;
        _description = description;
        _currentLevel = 0;
        _maxLevel = 0;
    }
}
