using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour   // �������� �� ���� ����Ʈ�� �̵� �� ���� ���������� �̵�
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DungeonManager.Instance.IsClear) // Ŭ���� �ߴ��� Ȯ��
        {
            if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)   // �浹ü�� �÷��̾����� Ȯ��
            {
                // ���� �������� �̵�
                // �� �ٽ� �ҷ������ ����
                //SceneController.instance.LoadGameScene();
                Debug.Log("���� �������� �̵�");
            }
        }
        else     // �׽�Ʈ�� 
        {
            Debug.Log("���� Ŭ���� �ȵ�");
        }
    }
}
