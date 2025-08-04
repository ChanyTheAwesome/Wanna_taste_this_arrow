using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBossClear : MonoBehaviour
{
    [SerializeField]
    private GameObject alley;
    [SerializeField]
    private GameObject alleyCollision;
    private EnemyManager _enemyManager;


    void Start()
    {
        GameObject alleyObj = GameObject.Find("Alley");
        GameObject alleyCollisionObj = GameObject.Find("AlleyCollision");

        if(alleyObj == null)
        {
            Debug.Log("��� ����");
        }
        if(alleyCollisionObj == null)
        {
            Debug.Log("��� �浹 ����");
        }

        alley = alleyObj;
        alleyCollision = alleyCollisionObj;
    }
    
    void Update()
    {
        if (!DungeonManager.Instance.IsClear)
        {
            return;
        }
        else
        {
            SetActiveAlley();
        }
    }

    void SetActiveAlley()
    {
        alley.SetActive(true);
        alleyCollision.SetActive(false);
    }
}
