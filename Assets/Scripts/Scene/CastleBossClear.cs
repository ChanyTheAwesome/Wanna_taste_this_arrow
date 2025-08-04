using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBossClear : MonoBehaviour
{
    [SerializeField]
    private GameObject alley;
    [SerializeField]
    private GameObject alleyCollision;

    void Start()
    {
        DungeonManager.Instance.CastleBossClear = this;

        GameObject alleyCollisionObj = GameObject.Find("AlleyCollision");

        if(alleyCollisionObj == null)
        {
            Debug.Log("길목 충돌 없음");
        }

        alleyCollision = alleyCollisionObj;
    }

    public void SetActiveAlley()
    {
        alley.SetActive(true);
        alleyCollision.SetActive(false);
    }
}
