using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    EnemyManager enemyManager;

    public int stageCount = 0;  // 현재 진행중인 스테이지 넘버, 스테이지 시작시에 올라가게 설정

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        enemyManager = FindObjectOfType<EnemyManager>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        StartGame();    // 게임 매니저를 파괴되지 않게 했는데 씬을 다시 불러와도 Start가 실행하나?
    }

    private void Update()
    {
        if (enemyManager.CheckEnemyExist())
        {
            Debug.Log("적 있음");
        }
        else
        {
            Debug.Log("적 없음");
        }
        DungeonManager.instance.CheckClearStage();

    }

    public void StartGame() // 처음에 게임 실행했을 때 실행할 것들, 스테이지나 던전 시작 아님, 최초 1회만 실행됨 다시 돌아와도 실행안됨
    {
        
    }

    public int CheckLayerObjectCount(LayerMask targetLayer)    // 특정 레이어를 가진 오브젝트의 개수를 카운팅
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();  // 모든 게임오브젝트를 가져옴, 비활성화된 것 제외

        int objectCount = 0;

        foreach (GameObject gameObject in allGameObjects)    // 게임오브젝트 순회
        {
            if ((targetLayer.value & (1 << gameObject.layer)) != 0)    // 레이어를 비교
            {
                objectCount++;  // 맞으면 카운트 증가
            }
        }
        return objectCount;
    }

    public void UpStageCount()
    {
        stageCount++;
        Debug.Log(stageCount);
    }

    public void DownStageCount()
    {
        stageCount--;
        Debug.Log(stageCount);
    }
}
