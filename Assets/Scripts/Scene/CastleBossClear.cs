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
        DungeonManager.Instance.CastleBossClear = this;

        //GameObject alleyObj = GameObject.Find("Alley");
        GameObject alleyCollisionObj = GameObject.Find("AlleyCollision");

        //if(alleyObj == null)
        //{
        //    Debug.Log("길목 없음");
        //}
        if(alleyCollisionObj == null)
        {
            Debug.Log("길목 충돌 없음");
        }

        //alley = alleyObj;
        alleyCollision = alleyCollisionObj;
    }
    
    void Update()
    {
        //if (!DungeonManager.Instance.IsClear)
        //{
        //    return;
        //}
        //else
        //{
        //    SetActiveAlley();
        //}
    }

    public void SetActiveAlley()
    {
        alley.SetActive(true);
        alleyCollision.SetActive(false);
    }
}
