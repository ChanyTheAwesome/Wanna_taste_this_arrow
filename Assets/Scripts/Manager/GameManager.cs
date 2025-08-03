using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    //private EnemyManager _enemyManager;

//#region 캐릭터 선택 만들기 임시 구역
//    private PlayerController _playerController;
//    public PlayerController PlayerController { get { return _playerController; } }

//    [SerializeField] private Sprite[] characterSprites;
//    [SerializeField] private RuntimeAnimatorController[] characterAnimators;
//    public Animator nowAnim;
//    private int _selectedIndex;
//    public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; } }
//#endregion
    
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
        //_enemyManager = FindObjectOfType<EnemyManager>();
        //_playerController = FindObjectOfType<PlayerController>();
        //DungeonManager.Instance.CurrentDungeonID = 1;
    }
    private void Start()
    {
        //SetPlayer();
        StartGame();
    }

    private void Update()
    {
        
    }

    public void StartGame() // 처음에 게임 실행했을 때 실행할 것들, 스테이지나 던전 시작 아님, 최초 1회만 실행됨 다시 돌아와도 실행안됨
    {
        CurrentState = UIState.Home;
        DungeonManager.Instance.CurrentDungeonID = 1;
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
    //    Debug.Log("SetPlayer");
    //    _playerController = FindObjectOfType<PlayerController>();
    //    nowAnim = _playerController.GetComponentInChildren<Animator>();
    //    if(_playerController == null)
    //    {
    //        Debug.Log("PlayerController not found in the scene.");
    //        return;
    //    }
    //}

    //public void SetCharacter()
    //{
    //    _playerController.GetComponentInChildren<SpriteRenderer>().sprite = characterSprites[_selectedIndex];
    //    nowAnim.runtimeAnimatorController = characterAnimators[_selectedIndex];
    //}
}
