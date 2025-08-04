using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    private List<AbilityData> _abilityList = new List<AbilityData>();
    private Dictionary<string, AbilityData> _abilityDict = new Dictionary<string, AbilityData>();

    private void Awake()
    {
        AddAbility("와~", "체력이 20만큼 회복 됩니다.", 0);
        AddAbility("와!!!", "최대 체력이 20만큼 증가합니다.", 5);
        AddAbility("나비처럼 날기", "더 빨리 움직입니다.", 5);
        AddAbility("급하다 급해", "더 빨리 공격합니다.", 5);
        AddAbility("삽 부스트", "삽이 더 빨리 날아갑니다.", 5);
        AddAbility("벌처럼 쏘기", "더 세게 때립니다.", 5);
        AddAbility("쌉", "던지는 삽의 개수를 늘립니다.", 5);
        AddAbility("샵", "삽이 통과합니다.", 5);
        AddAbility("벽 반삽", "벽에 맞으면 반사합니다.", 5);
        AddAbility("후방주의", "뒤로도 삽을 던집니다.", 5);
        AddAbility("왜 이러시는 건데요", "적을 맞히면 튕겨져 근처의 적을 공격합니다.", 5);        
    }

    private void AddAbility(string name, string description, int maxLevel)
    {
        AbilityData data = new AbilityData(name, description, maxLevel);
        _abilityList.Add(data);
        _abilityDict.Add(name, data);
    }
    private void AddAbility(string name, string description)
    {
        AbilityData data = new AbilityData(name, description);
        _abilityList.Add(data);
        _abilityDict.Add(name, data);
    }

    public List<AbilityData> GetRandomAbility(int count)
    {
        List<AbilityData> available = new List<AbilityData>();

        foreach (AbilityData ability in _abilityList)
        {
            if (ability._maxLevel == 0) available.Add(ability);

            if (ability._currentLevel < ability._maxLevel)
            {
                available.Add(ability);
            }
        }

        List<AbilityData> result = new List<AbilityData>();
        while (result.Count < count && available.Count > 0)
        {
            int index = Random.Range(0, available.Count);
            result.Add(available[index]);
            available.RemoveAt(index);
        }

        return result;
    }

    public AbilityData GetAbilityData(string abilityName)
    {
        if(_abilityDict.ContainsKey(abilityName)) return _abilityDict[abilityName];
        Debug.LogWarning("Not found ability" + abilityName);
        return null;
    }

    public void ApplyAbility(string abilityName)
    {
        if(_abilityDict.ContainsKey(abilityName))
        {
            AbilityData ability = _abilityDict[abilityName];
            if(ability._currentLevel < ability._maxLevel)
            {
                ability._currentLevel++;
                Debug.Log($"{ability._abilityName} -> Lv. {ability._currentLevel}");
            }
        }
    }
}
