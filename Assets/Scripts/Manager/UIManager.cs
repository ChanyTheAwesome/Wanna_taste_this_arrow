using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI ���� enum, Ȩ, ������, �������� ������ ���� ���� < �ʿ��Ѱ� �𸣰���
{
    Home,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    //public UIState currentState;

    HomeUI _homeUI;
    GameUI _gameUI;
    GameOverUI _gameOverUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _homeUI = GetComponentInChildren<HomeUI>(true);
        //_homeUI.Init(/*this*/);
        _gameUI = GetComponentInChildren<GameUI>(true);
        //_gameUI.Init(/*this*/);
        _gameOverUI = GetComponentInChildren<GameOverUI>(true);
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
        // �Ͻ������� �޴� UI ����
        ChangeState(UIState.Game);
    }

    public void SetGameOver()   // ���� ������ UI ����
    {
        // �Ͻ����� �� ���ӿ��� UI ����
        // �����ߴٴ� UI ���(������ �� ����?, Ȩ���� ���ư��� ��ư)
        ChangeState(UIState.GameOver);
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
