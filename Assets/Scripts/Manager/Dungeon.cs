using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int BaseEnemyCount {  get; private set; }
    public int EnemyCount => BaseEnemyCount + GameManager.instance.stageCount / 3;  // ���������� �����ϴ� �� ī��Ʈ 3���������� 1 ������ ����

    public Dungeon(int id, string name, int maxStageCount, int baseEnemyCount)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        BaseEnemyCount = baseEnemyCount;
    }
}
