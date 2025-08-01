using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageArea : MonoBehaviour   // �������� �� ���� ����Ʈ�� �̵� �� ���� ���������� �̵�
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DungeonManager.Instance.IsClear) // Ŭ���� �ߴ��� Ȯ��
        {
            if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)   // �浹ü�� �÷��̾����� Ȯ��
            {
                // ���� ���������� ������ �ִ� ������������ �۴ٸ� ���� �������� �̵�
                if(GameManager.Instance.StageCount < DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].MaxStageCount)
                {
                    DungeonManager.Instance.StartStage();
                }
                // ������ ���������� ���� ������
                else
                {
                    DungeonManager.Instance.ExitDungeon();
                }
            }
        }
    }
}
