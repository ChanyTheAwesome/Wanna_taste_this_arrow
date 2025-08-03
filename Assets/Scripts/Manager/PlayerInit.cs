using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    [SerializeField] private FollowCamera followCamera;

    public static bool isSetPlayer = false;

    private void Start()
    {
        Debug.Log("Player Init �÷��̾� ����");   // �׽�Ʈ��
        PlayerManager.Instance.InitPlayer();
        followCamera.target = PlayerManager.Instance.PlayerController.transform;
        EnemyManager._playerController = PlayerManager.Instance.PlayerController;
        isSetPlayer = true;
    }
}
