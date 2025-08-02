using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    private static PlayerInit instance;
    public static PlayerInit Instance => instance;

    [SerializeField] private FollowCamera followCamera;

    public bool isSetPlayer = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerManager.Instance.InitPlayer();
        followCamera.target = PlayerManager.Instance.PlayerController.transform;
        isSetPlayer = true;
    }
}
