using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount { get; private set; }
    public int EnemyIncreaseInterval { get; private set; }    // 몇 스테이지마다 적 수를 늘릴건지
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / EnemyIncreaseInterval;  // 스테이지당 증가하는 적 카운트

    public float IncreaseStat { get; private set; }   // 스테이지당 스탯 증가율

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
