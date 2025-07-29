using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList;

    public bool isClear = false;

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
        dungeonList.Add(new Dungeon(1, "1단계 던전", 10, 6));
        dungeonList.Add(new Dungeon(2, "2단계 던전", 10, 6));
        dungeonList.Add(new Dungeon(3, "3단계 던전", 10, 6));
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

    public void ClearStage()
    {
        isClear = true;
        // 다 처리했다는 UI 띄우기?
    }

    public void StartDungeon(Dungeon dungeon)
    {
        GameManager.instance.stageCount = 1;
        StartStage(dungeon.MaxStageCount);
    }

    public IEnumerable StartStage(int maxStageCount)
    {
        //int stageCount = 0;
        // UIState 게임으로 설정하기
        // 스테이지 반복 > 반복문 해서 마지막 스테이지면 보스방
        for(int i = 1; i <= maxStageCount; i++)    // 코루틴 써야겠다 yield return new WaitUntil(bool predicate) << 이용하면 될듯
        {
            //stageCount++;
            isEnd = false;
            // 플레이어 위치 세팅

            // 몬스터 스폰
            if(i == maxStageCount) // 마지막 스테이지일 때
            {
                // 보스 스테이지 시작
            }
            else    // 보스전 아닐때
            {
                // 맵 장애물 재배치
                // 일반 몬스터 배치
            }
            //yield return stageCount;
            yield return new WaitUntil(() => isEnd);  // 코루틴 사용해서 대기하기, 다음 스테이지 넘어갈려면 isEnd true로 바꿔주기
        }
        
        // 죽으면 나가기 << 계속 체크해야되니 update로? 아니면 피 깎일때 체크하는 메서드 만들고 추가?
        // 에너미 쪽에서 만든 공격을 받았을 때 체력이 감소하는 로직에서 죽는지 확인
    }
}
