using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance => instance;

    public List<Dungeon> DungeonList = new List<Dungeon>();

    private bool _isClear = false;
    public bool IsClear => _isClear;    // 스테이지 클리어 했는지

    private int _currentDungeonID = 0;
    public int CurrentDungeonID {
        get { return _currentDungeonID; }
        set { _currentDungeonID = value; }
    }
    
    private bool _isFirstStage;

    private EnemyManager _enemyManager;
    private EnemyController _enemyController;

    //Suggestion
    
    public Dictionary<int, Dungeon> DungeonDict = new Dictionary<int, Dungeon>();
    
    private void AddDungeonDict()
    {
        DungeonDict.Add(1, new Dungeon(1, "1단계 던전", 10, 6, 3, 1.05f));
        DungeonDict.Add(2, new Dungeon(2, "2단계 던전", 10, 6, 3, 1.05f));
        DungeonDict.Add(3, new Dungeon(3, "3단계 던전", 10, 6, 3, 1.05f));
    }
     
    //Or try to use Json/newtonJson to read json data.
     

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

        Init();
    }
    private void Init()
    {
        AddDungeonDict();

        _enemyManager = FindObjectOfType<EnemyManager>();
        _enemyController = FindObjectOfType<EnemyController>();
        //These are too heavy to deal with, and some minor potential errors
    }

    public void CheckClearStage()    // 스테이지 클리어 확인, 몬스터 죽을 때마다 사용하면 될듯
    {
        // 에너미 있는지 확인
        if (_enemyManager.CheckEnemyExist()) // 적이 남아있다면
        {
            Debug.Log("클리어 안됨");    // 테스트용
            return;
        }
        else    // 적이 없다면
        {
            Debug.Log("클리어");   // 테스트용
            ClearStage();
        }   
    }

    public void StartDungeon(int dungeonID)  // 던전 시작시 실행할 것들
    {
        Debug.Log(dungeonID);   // 테스트용
        _isFirstStage = true;
        _currentDungeonID = dungeonID;   // dungeonID를 어떻게 가져와야될까
        StartStage();
    }

    public void StartStage()    // 스테이지 시작시 실행할 것들
    {
        _isClear = false;
        // 몬스터 스탯 올리기
        if (!_isFirstStage)  // 첫 스테이지가 아니라면 = 첫 스테이지는 기본 체력만 가지기
        {
            _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);   // ID로 던전 찾아서 그 던전의 IncreaseStat값 가져와서 SetEnemyHealth 실행하기
        }
        GameManager.Instance.StageCount++;
        //UIManager.Instance.SetGame();   // 어차피 씬을 로드하는 식으로 넘어가면 UI는 초기화 될거임 씬 불러오고 설정해야될듯
        _isFirstStage = false; //<- Does this variable need to be here? If not, consider moving it to the top. 
        SceneController.Instance.LoadGameScene();
    }

    public void ClearStage()    // 스테이지 클리어시 실행할 것들
    {
        Debug.Log("스테이지 클리어");
        _isClear = true;
        UIManager.Instance.SetClearStage();
    }

    public void GameOver()  // 게임오버시 실행할 것들
    {
        UIManager.Instance.SetGameOver();
    }

    public void ExitDungeon()   // 던전에서 나갈 때 실행할 것들
    {
        // 플레이어 레벨 1로 만들기
        // 플레이어 경험치 0으로 만들기
        PlayerManager.instance.ResetPlayer();
        // stageCount 0으로 맞추기
        GameManager.Instance.StageCount = 0;
        // currentDungeonID 0으로 맞추기
        _currentDungeonID = 0;
        // 홈 화면 불러오기
        SceneController.Instance.LoadMainScene();
    }

    public void ChangeStat()    // 테스트용 메서드
    {
        _currentDungeonID = 1;//Meaning?
        //_enemyController.SetEnemyHealth(DungeonList.Find(d => d.ID == _currentDungeonID).IncreaseStat);
        _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);
        _currentDungeonID = 0;
    }
}