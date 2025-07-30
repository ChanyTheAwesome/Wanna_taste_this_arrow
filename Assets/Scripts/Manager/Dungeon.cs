using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount { get; private set; }
    public int EnemyIncreaseInterval { get; private set; }    // �� ������������ �� ���� �ø�����
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / EnemyIncreaseInterval;  // ���������� �����ϴ� �� ī��Ʈ

    public float IncreaseStat { get; private set; }   // ���������� ���� ������

    public Dungeon(int id, string name, int maxStageCount, int baseEnemyCount, int enemyIncreaseInterval, float increaseStat)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        BaseEnemyCount = baseEnemyCount;
        EnemyIncreaseInterval = enemyIncreaseInterval;
        IncreaseStat = increaseStat;
    }
}
