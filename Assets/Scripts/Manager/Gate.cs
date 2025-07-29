using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour   // 스테이지 다 깨고 게이트로 이동 시 다음 스테이지로 이동
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DungeonManager.instance.isClear) // 클리어 했는지 확인
        {
            if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)   // 충돌체가 플레이어인지 확인
            {
                // 다음 스테이지 이동
                DungeonManager.instance.isEnd = true;
                // 아니면 그냥 씬을 불러오기?
            }
        }
    }
}
