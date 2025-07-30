using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount {  get; private set; }
    public int IncreaseEnemyCount {  get; private set; }    // �� ���������� �� ���� �ø�����
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / IncreaseEnemyCount;  // ���������� �����ϴ� �� ī��Ʈ

    public Dungeon(int id, string name, int maxStageCount, int baseEnemyCount, int increaseEnemyCount)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        BaseEnemyCount = baseEnemyCount;
        IncreaseEnemyCount = increaseEnemyCount;
    }
}
