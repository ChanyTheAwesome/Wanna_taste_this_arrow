using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private EnemyManager _enemyManager;
    //private PlayerController _playerController;

    //public PlayerController PlayerController { get { return _playerController; } }

    [SerializeField] private GameObject[] characterPrefabs;
    private int selectedIndex = 0;

    private int stageCount = 0;
    public int StageCount
    {
        get { return stageCount; }
        set { stageCount = value; }
    }
    // 현재 진행중인 스테이지 넘버, 스테이지 시작시에 올라가게 설정

    private UIState _currentState;

    public UIState CurrentState { get { return _currentState; } set { _currentState = value; } }

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
        _enemyManager = FindObjectOfType<EnemyManager>();
        //_playerController = FindObjectOfType<PlayerController>();
        //DungeonManager.Instance.CurrentDungeonID = 1;

        CurrentState = UIState.Home;
    }
    private void Start()
    {
        DungeonManager.Instance.CurrentDungeonID = 1;
    }

    private void Update()
    {
        
    }

    public void StartGame() // 처음에 게임 실행했을 때 실행할 것들, 스테이지나 던전 시작 아님, 최초 1회만 실행됨 다시 돌아와도 실행안됨
    {
        
    }

    //public void UpStageCount()  // 테스트용
    //{
    //    stageCount++;
    //    Debug.Log(stageCount);
    //}

    //public void DownStageCount()    // 테스트용
    //{
    //    stageCount--;
    //    Debug.Log(stageCount);
    //}

    //public void CheckEnemy()    // 테스트용
    //{
    //    DungeonManager.Instance.CheckClearStage();
    //}

    //public void SetPlayer() // 테스트용
    //{
    //    _playerController = FindObjectOfType<PlayerController>();
    //}
}
