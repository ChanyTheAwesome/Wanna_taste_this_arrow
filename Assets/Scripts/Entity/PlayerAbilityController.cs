using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    private List<AbilityData> _abilityList = new List<AbilityData>();
    private Dictionary<string, AbilityData> _abilityDict = new Dictionary<string, AbilityData>();

    private void Awake()
    {
        AddAbility("��~", "ü���� 20��ŭ ȸ�� �˴ϴ�.", 0);
        AddAbility("��!!!", "�ִ� ü���� 20��ŭ �����մϴ�.", 5);
        AddAbility("����ó�� ����", "�� ���� �����Դϴ�.", 5);
        AddAbility("���ϴ� ����", "�� ���� �����մϴ�.", 5);
        AddAbility("�� �ν�Ʈ", "���� �� ���� ���ư��ϴ�.", 5);
        AddAbility("��ó�� ���", "�� ���� �����ϴ�.", 5);
        AddAbility("��", "������ ���� ������ �ø��ϴ�.", 5);
        AddAbility("��", "���� ����մϴ�.", 5);
        AddAbility("�� �ݻ�", "���� ������ �ݻ��մϴ�.", 5);
        AddAbility("�Ĺ�����", "�ڷε� ���� �����ϴ�.", 5);
        AddAbility("�� �̷��ô� �ǵ���", "���� ������ ƨ���� ��ó�� ���� �����մϴ�.", 5);        
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
