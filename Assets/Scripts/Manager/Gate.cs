using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour   // �������� �� ���� ����Ʈ�� �̵� �� ���� ���������� �̵�
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
        if (DungeonManager.instance.isClear) // Ŭ���� �ߴ��� Ȯ��
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
