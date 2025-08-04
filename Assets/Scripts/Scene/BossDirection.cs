using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    [SerializeField]
    private float pivotPosY;
    private GameObject player;
    private const string playerName = "Player(Clone)";
    private StatHandler playerStatHandler;
    [SerializeField]
    private Camera directionCamera;
    private FollowCamera followCamera;
    [SerializeField]
    private GameObject bossDirect;
    [SerializeField]
    private GameObject bossCollision;
    private GameObject target;
    private bool isDirecting = false;
    //public GameObject test;
    
    void Start()
    {
        //Vector3 vector3 = new Vector3(0, 48, 0);
        //Instantiate(test, vector3, Quaternion.identity);

        GameObject playerObject = GameObject.Find(playerName);
        player = playerObject;
        playerStatHandler = player.GetComponent<StatHandler>();
        followCamera = directionCamera.GetComponent<FollowCamera>();
        bossCollision = GameObject.Find("BossCollision");
        //if (player == null) Debug.LogError("�÷��̾� ����");
        //if (playerStatHandler == null) Debug.LogError("�����ڵ鷯 �� �پ��� �̳༮��");
        //if (bossDirect == null) Debug.LogError("bossDirect ������Ʈ�� �� �پ��� �̳༮��");
        //if (followCamera == null) Debug.LogError("followcamera�� �� �پ��� �̳༮��");
        //if (target == null) Debug.Log("Ÿ�� �� ��");
    }

    void Update()
    {
        if (isDirecting) return;

        if(player.transform.position.y >= pivotPosY)
        {
            StartCoroutine(BossDirecting());
            
        }
    }
    IEnumerator BossDirecting()
    {

        target = GameObject.Find("Boss3(Clone)");
        isDirecting = true;
        playerStatHandler.Speed = 0;
        if(followCamera == null) Debug.Log("sdfsdfsdf");
        if (target == null) Debug.Log("target null");
        followCamera.target = target.transform;
        bossDirect.SetActive(true);
        yield return new WaitForSeconds(4f); // ��ٸ�
        playerStatHandler.Speed = PlayerController.moveSpeed;
        followCamera.target = player.transform;
        bossCollision.SetActive(false);
        yield return new WaitForSeconds(6f); // ��ٸ�
        GetComponent<BossDirection>().enabled = false;
    }
}
