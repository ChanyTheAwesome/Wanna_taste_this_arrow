using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList = new List<Dungeon>();

    public bool isClear = false;    // 스테이지 클리어 했는지

    public int currentDungeonID = 0;

    bool isFirstStage;

    EnemyManager enemyManager;
    EnemyController enemyController;

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

        dungeonList.Add(new Dungeon(1, "1단계 던전", 10, 6, 3, 1.05f));
        dungeonList.Add(new Dungeon(2, "2단계 던전", 10, 6, 3, 1.05f));
        dungeonList.Add(new Dungeon(3, "3단계 던전", 10, 6, 3, 1.05f));

        enemyManager = FindObjectOfType<EnemyManager>();
        enemyController = FindObjectOfType<EnemyController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClearStage()    // 스테이지 클리어 확인, 몬스터 죽을 때마다 사용하면 될듯
    {
        // 에너미 있는지 확인
        if (enemyManager.CheckEnemyExist()) // 적이 남아있다면
        {
            return;
        }
        else    // 적이 없다면
        {
            ClearStage();
        }   
    }

    public void StartDungeon(int dungeonID)  // 던전 시작시 실행할 것들
    {
        isFirstStage = true;
        currentDungeonID = dungeonID;   // dungeonID를 어떻게 가져와야될까
        StartStage();
    }

    public void StartStage()    // 스테이지 시작시 실행할 것들
    {
        isClear = false;
        // 몬스터 스탯 올리기
        if (!isFirstStage)  // 첫 스테이지가 아니라면 = 첫 스테이지는 기본 체력만 가지기
        {
            enemyController.SetEnemyHealth(dungeonList.Find(d => d.ID == currentDungeonID).IncreaseStat);   // ID로 던전 찾아서 그 던전의 IncreaseStat값 가져와서 SetEnemyHealth 실행하기
        }
        GameManager.instance.stageCount++;
        UIManager.instance.SetGame();
        isFirstStage = false;
    }

    public void ClearStage()    // 스테이지 클리어시 실행할 것들
    {
        Debug.Log("스테이지 클리어");
        isClear = true;
        UIManager.instance.SetClearStage();
    }

    public void GameOver()  // 게임오버시 실행할 것들
    {
        UIManager.instance.SetGameOver();
    }

    public void ExitDungeon()   // 던전에서 나갈 때 실행할 것들
    {
        // 플레이어 레벨 1로 만들기
        // 플레이어 경험치 0으로 만들기
        PlayerManager.instance.ResetPlayer();
        // stageCount 0으로 맞추기
        GameManager.instance.stageCount = 0;
        // currentDungeonID 0으로 맞추기
        currentDungeonID = 0;
        // 홈 화면 불러오기
        SceneController.instance.LoadMainScene();
    }

    public void ChangeStat()
    {
        currentDungeonID = 1;
        enemyController.SetEnemyHealth(dungeonList.Find(d => d.ID == currentDungeonID).IncreaseStat);
        currentDungeonID = 0;
    }
}
