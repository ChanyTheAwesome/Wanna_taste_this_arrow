using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageArea : MonoBehaviour   // �������� �� ���� ����Ʈ�� �̵� �� ���� ���������� �̵�
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹");    // �׽�Ʈ��
        if (DungeonManager.Instance.IsClear) // Ŭ���� �ߴ��� Ȯ��
        {
            Debug.Log("Ŭ����� ��");    // �׽�Ʈ��
            if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)   // �浹ü�� �÷��̾����� Ȯ��
            {
                Debug.Log("�̵� �õ�"); // �׽�Ʈ��
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
