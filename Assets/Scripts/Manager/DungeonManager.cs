using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList;

    public bool isClear = false;    // 이거 쓸데 있나?

    public bool isEnd = false;  // 몬스터 스폰한 거 다 잡았는지 여부, true로 바꾸면 바로 다음 스테이지 이동

    //public int stageCount = 0;

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

        dungeonList.Clear();
        dungeonList.Add(new Dungeon(1, "1단계 던전", 10, 6, 3));
        dungeonList.Add(new Dungeon(2, "2단계 던전", 10, 6, 3));
        dungeonList.Add(new Dungeon(3, "3단계 던전", 10, 6, 3));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClearStage()    // 스테이지 클리어
    {
        // 에너미 있는지 확인

        // 남아있으면 돌아가기
        // 없으면 클리어
        ClearStage();
    }

    public void StartDungeon()  // 던전 시작시 실행할 것들
    {
        StartStage();
    }

    public void StartStage()    // 스테이지 시작시 실행할 것들
    {
        GameManager.instance.stageCount++;
        UIManager.instance.SetGame();
    }

    public void ClearStage()    // 스테이지 클리어시 실행할 것들
    {
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
        // 홈 화면 불러오기
        SceneController.instance.LoadMainScene();
    }
}
