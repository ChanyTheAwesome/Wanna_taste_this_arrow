using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    [SerializeField]
    private float pivotPosY;
    private GameObject player;
    private const string playerName = "Farmer0";
    private StatHandler playerStatHandler;
    [SerializeField]
    private Camera directionCamera;
    private FollowCamera followCamera;
    [SerializeField]
    private GameObject bossDirect;
    private GameObject target;
    private bool isDirecting = false;
    public GameObject test;
    
    void Start()
    {
        Vector3 vector3 = new Vector3(0, 48, 0);
        Instantiate(test, vector3, Quaternion.identity);

        GameObject playerObject = GameObject.Find(playerName);
        player = playerObject;
        playerStatHandler = player.GetComponent<StatHandler>();
        followCamera = directionCamera.GetComponent<FollowCamera>();
        target = GameObject.Find("Boss(Clone)");
        if (player == null) Debug.LogError("�÷��̾� ����");
        if (playerStatHandler == null) Debug.LogError("�����ڵ鷯 �� �پ��� �̳༮��");
        if (bossDirect == null) Debug.LogError("bossDirect ������Ʈ�� �� �پ��� �̳༮��");
        if (followCamera == null) Debug.LogError("followcamera�� �� �پ��� �̳༮��");
        if (target == null) Debug.Log("Ÿ�� �� ��");
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
        isDirecting = true;

        playerStatHandler.Speed = 0;
        followCamera.target = target.transform;
        bossDirect.SetActive(true);
        yield return new WaitForSeconds(4f); // ��ٸ�
        playerStatHandler.Speed = 3f;
        followCamera.target = player.transform;
        yield return new WaitForSeconds(6f); // ��ٸ�
        GetComponent<BossDirection>().enabled = false;
    }
}
