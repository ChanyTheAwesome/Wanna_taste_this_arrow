using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount {  get; private set; }
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / 3;  // 스테이지당 증가하는 적 카운트 3스테이지당 1 증가로 설정

    public Dungeon(int id, string name, int maxStageCount, int baseEnemyCount)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        BaseEnemyCount = baseEnemyCount;
    }
}
