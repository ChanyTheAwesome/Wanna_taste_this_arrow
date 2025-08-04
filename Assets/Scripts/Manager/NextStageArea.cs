using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageArea : MonoBehaviour   // 스테이지 다 깨고 게이트로 이동 시 다음 스테이지로 이동
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DungeonManager.Instance.IsClear) // 클리어 했는지 확인
        {
            if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)   // 충돌체가 플레이어인지 확인
            {
                if (DungeonManager.Instance.CheckBossStage())   // 보스 스테이지면 던전 나가기
                {
                    DungeonManager.Instance.ExitDungeon();
                }
                else    // 일반 스테이지면 스테이지 실행
                {
                    DungeonManager.Instance.StartStage();
                }
            }
        }
    }
}
