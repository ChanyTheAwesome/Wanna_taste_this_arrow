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

//#region ĳ���� ���� ����� �ӽ� ����
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
    // ���� �������� �������� �ѹ�, �������� ���۽ÿ� �ö󰡰� ����

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

    public void StartGame() // ó���� ���� �������� �� ������ �͵�, ���������� ���� ���� �ƴ�, ���� 1ȸ�� ����� �ٽ� ���ƿ͵� ����ȵ�
    {
        CurrentState = UIState.Home;
        DungeonManager.Instance.CurrentDungeonID = 1;
    }

    //public void UpStageCount()  // �׽�Ʈ��
    //{
    //    stageCount++;
    //    Debug.Log(stageCount);
    //}

    //public void DownStageCount()    // �׽�Ʈ��
    //{
    //    stageCount--;
    //    Debug.Log(stageCount);
    //}

    //public void CheckEnemy()    // �׽�Ʈ��
    //{
    //    DungeonManager.Instance.CheckClearStage();
    //}

    //public void SetPlayer() // �׽�Ʈ��
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
