using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance => instance;

    public List<Dungeon> DungeonList = new List<Dungeon>();

    private bool _isClear = false;
    public bool IsClear => _isClear;    // 스테이지 클리어 했는지

    private int _currentDungeonID = 1;
    public int CurrentDungeonID {
        get { return _currentDungeonID; }
        set { _currentDungeonID = value; }
    }
    
    private bool _isFirstStage;

    public bool IsFirstStage { get { return _isFirstStage; } set { _isFirstStage = value; } }

    private EnemyManager _enemyManager;
    public EnemyManager EnemyManager { get { return _enemyManager; } set { _enemyManager = value; } }
    private EnemyController _enemyController;

    public Gate gate;

    [SerializeField] private Material caveMaterial;
    public Material CaveMaterial { get { return caveMaterial; } }

    //Suggestion

    public Dictionary<int, Dungeon> DungeonDict = new Dictionary<int, Dungeon>();
    
    private void AddDungeonDict()   // 테스트용으로 스테이지 3개로 설정, 이후에 다시 변경 필요
    {
        DungeonDict.Add(1, new Dungeon(1, "잠자는 숲속의 좀비^^", 10, 6, 3, 1.05f));
        DungeonDict.Add(2, new Dungeon(2, "번호따고 싶은 석상^^", 10, 6, 3, 1.05f));
        DungeonDict.Add(3, new Dungeon(3, "야 묘비 삽 넣을게^^", 10, 6, 3, 1.05f));
    }
     
    //Or try to use Json/newtonJson to read json data.
     

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

    public void StartDungeon()  // 던전 시작시 실행할 것들
    {
        Debug.Log($"{CurrentDungeonID} 던전 입장");   // 테스트용
        _isFirstStage = true;
        StartStage();
    }

    public void StartStage()    // 스테이지 시작시 실행할 것들
    {
        Debug.Log("스테이지 시작");   // 테스트용
        _isClear = false;
        // 몬스터 스탯 올리기
        //if (!_isFirstStage)  // 첫 스테이지가 아니라면 = 첫 스테이지는 기본 체력만 가지기
        //{
        //    _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);   // ID로 던전 찾아서 그 던전의 IncreaseStat값 가져와서 SetEnemyHealth 실행하기
        //}
        GameManager.Instance.StageCount++;
        //UIManager.Instance.SetGame();   // 어차피 씬을 로드하는 식으로 넘어가면 UI는 초기화 될거임 씬 불러오고 설정해야될듯
        //_isFirstStage = false;
        //if(GameManager.Instance.StageCount < DungeonDict[CurrentDungeonID].MaxStageCount)   // 마지막 스테이지가 아니면 일반 씬 불러오기
        //{
        //    SceneController.Instance.LoadDungeonScene();
        //}
        //else    // 마지막 스테이지면 보스 씬 불러오기
        //{
        //    SceneController.Instance.LoadBossScene();
        //}
        if (CheckBossStage())   // 보스 스테이지면 보스 씬 불러오기
        {
            SceneController.Instance.LoadBossScene();
        }
        else    // 일반 스테이지면 일반 씬 불러오기
        {
            SceneController.Instance.LoadDungeonScene();
        }
    }

    public void ClearStage()    // 스테이지 클리어시 실행할 것들
    {
        Debug.Log("스테이지 클리어");  // 테스트용
        _isClear = true;
        gate.OpenGate();
        AchievementManager.Instance.OnStageClear(GameManager.Instance.StageCount);
        UIManager.Instance.SetClear();
    }

    public void GameOver()  // 게임오버시 실행할 것들
    {
        UIManager.Instance.SetGameOver();
    }

    public void ExitDungeon()   // 던전에서 나갈 때 실행할 것들
    {
        // 플레이어 레벨 1로 만들기
        // 플레이어 경험치 0으로 만들기
        PlayerManager.Instance.ResetPlayer();
        // stageCount 0으로 맞추기
        GameManager.Instance.StageCount = 0;
        // currentDungeonID 0으로 맞추기
        _currentDungeonID = 1;
        // 플레이어 외형 초기화하기
        PlayerManager.Instance.SelectedIndex = 0;
        PlayerManager.Instance.SetCharacter();
        UIManager.Instance.SetHome();
        // 홈 화면 불러오기
        SceneController.Instance.LoadMainScene();
    }

    public bool CheckBossStage(int num = 0)    // 마지막 보스 스테이지면 true, 아니면 false, num 매개변수로 어느 시점에서 계산할 건지 조절
    {
        if (GameManager.Instance.StageCount == (DungeonDict[CurrentDungeonID].MaxStageCount + num))
        {
            return true;
        }
        else return false;
    }

    public void ChangeStat()    // 테스트용 메서드
    {
        //_currentDungeonID = 1;//Meaning?
        //_enemyController.SetEnemyHealth(DungeonList.Find(d => d.ID == _currentDungeonID).IncreaseStat);
        _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);
        //_currentDungeonID = 0;
    }
}