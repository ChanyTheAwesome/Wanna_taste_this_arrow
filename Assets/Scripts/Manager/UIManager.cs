using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI ���� enum, Ȩ, ������, �������� ������ ���� ���� < �ʿ��Ѱ� �𸣰���
{
    Home,
    Game,
    GameOver,
    Clear
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    //public UIState currentState;

    HomeUI _homeUI;
    public HomeUI HomeUI { get { return _homeUI; } set { _homeUI = value; } }

    GameUI _gameUI;
    public GameUI GameUI { get { return _gameUI; } set { _gameUI = value; } }

    GameOverUI _gameOverUI;
    public GameOverUI GameOverUI { get { return _gameOverUI; } set{ _gameOverUI = value; } }

    ClearUI _clearUI;
    public ClearUI ClearUI { get { return _clearUI; } set { _clearUI = value; } }

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

        //_homeUI = GetComponentInChildren<HomeUI>(true);
        //_homeUI.Init(/*this*/);
        //_gameUI = GetComponentInChildren<GameUI>(true);
        //_gameUI.Init(/*this*/);
        //_gameOverUI = GetComponentInChildren<GameOverUI>(true);
        //_gameOverUI.Init(/*this*/);
    }

    private void Start()
    {
        SetHome();
    }

    public void SetHome()
    {
        ChangeState(UIState.Home);
    }

    public void SetGame()   // ������ UI ����
    {
        //_gameUI.SetGame();
        // �Ͻ������� �޴� UI ����
        ChangeState(UIState.Game);
    }

    public void SetClear()
    {
        _clearUI.SetClearUI();
        ChangeState(UIState.Clear);
    }

    public void SetGameOver()   // ���� ������ UI ����
    {
        // �Ͻ����� �� ���ӿ��� UI ����
        _gameOverUI.SetGameOverUI();
        // �����ߴٴ� UI ���(������ �� ����?, Ȩ���� ���ư��� ��ư)
        ChangeState(UIState.GameOver);
    }

    public void SetAchievement(string name, string description)
    {
        Debug.Log("SetAchievement");
        _gameUI.SetAchievementUI(name, description);
    }

    public void SetClearStage() // �������� Ŭ���� �� UI ����
    {
        // �������� Ŭ�����ߴٴ� ������ ����
    }

    public void ChangeState(UIState state)
    {
        GameManager.Instance.CurrentState = state;
        //_homeUI.SetActive(GameManager.Instance.CurrentState);
        //_gameUI.SetActive(GameManager.Instance.CurrentState);
        //_gameOverUI.SetActive(GameManager.Instance.CurrentState);
    }
}
