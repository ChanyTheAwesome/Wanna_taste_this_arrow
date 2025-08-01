using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Button spawnButton;
    public TutorialEnemySpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton.onClick.AddListener(CallEnemySpawn);
    }

    public void CallEnemySpawn()
    {
        spawner.EnemySpawn();
    }
}
