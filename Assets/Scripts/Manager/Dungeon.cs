using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount {  get; private set; }
    public int IncreaseEnemyCount {  get; private set; }    // 몇 스테이지당 적 수를 늘릴건지
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / IncreaseEnemyCount;  // 스테이지당 증가하는 적 카운트

    public Dungeon(int id, string name, int maxStageCount, int baseEnemyCount, int increaseEnemyCount)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        BaseEnemyCount = baseEnemyCount;
        IncreaseEnemyCount = increaseEnemyCount;
    }
}
