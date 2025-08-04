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
    
    void Start()
    {
        GameObject playerObject = GameObject.Find(playerName);
        player = playerObject;
        playerStatHandler = player.GetComponent<StatHandler>();
        followCamera = directionCamera.GetComponent<FollowCamera>();
        bossCollision = GameObject.Find("BossCollision");
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
        followCamera.target = target.transform;
        bossDirect.SetActive(true);
        yield return new WaitForSeconds(4f); // 기다림
        playerStatHandler.Speed = PlayerController.moveSpeed;
        followCamera.target = player.transform;
        bossCollision.SetActive(false);
        yield return new WaitForSeconds(6f); // 기다림
        GetComponent<BossDirection>().enabled = false;
    }
}
