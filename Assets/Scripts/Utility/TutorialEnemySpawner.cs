using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private void Start()
    {

    }

    public void EnemySpawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
