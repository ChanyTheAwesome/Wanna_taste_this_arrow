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
        if (player == null) Debug.LogError("플레이어 없음");
        if (playerStatHandler == null) Debug.LogError("스탯핸들러 안 붙었다 이녀석아");
        if (bossDirect == null) Debug.LogError("bossDirect 오브젝트가 안 붙었다 이녀석아");
        if (followCamera == null) Debug.LogError("followcamera가 안 붙었다 이녀석아");
        if (target == null) Debug.Log("타겟 안 붙");
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
        yield return new WaitForSeconds(4f); // 기다림
        playerStatHandler.Speed = 3f;
        followCamera.target = player.transform;
        yield return new WaitForSeconds(6f); // 기다림
        GetComponent<BossDirection>().enabled = false;
    }
}
