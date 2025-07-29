using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int ID {  get; private set; }
    public string Name {  get; private set; }
    public int MaxStageCount { get; private set; }
    public int EnemyCount {  get; private set; }

    public Dungeon(int id, string name, int maxStageCount, int enemyCount)
    {
        ID = id;
        Name = name;
        MaxStageCount = maxStageCount;
        EnemyCount = enemyCount;
    }
}
