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

    public UIState currentState;

    HomeUI _homeUI;
    GameUI _gameUI;
    GameOverUI _gameOverUI;

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

        _homeUI = GetComponentInChildren<HomeUI>(true);
        _homeUI.Init(/*this*/);
        _gameUI = GetComponentInChildren<GameUI>(true);
        //gameUI.Init(this);
        _gameOverUI = GetComponentInChildren<GameOverUI>(true);
        //gameOverUI.Init(this);

        //ChangeState(UIState.Home);
    }

    public void OnClickMenu()   // �޴� ��ư Ŭ��
    {
        StopGame();
        // �޴� UI ����
    }

    public void OnClickStop()   // �Ͻ����� ��ư Ŭ��
    {
        StopGame();
        // �Ͻ����� UI ����
    }

    public void OnClickStart()  // ���� ��ư Ŭ��
    {
        // ���� �� �ҷ�����
        SceneController.Instance.LoadGameScene();
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // ������� �ð� �ٽ� ����
        // Ȩ �� �ҷ�����
        DungeonManager.Instance.ExitDungeon();
    }

    public void OnClickOption() // �ɼ� ��ư Ŭ��
    {
        StopGame();
        // �ɼ� UI ����
    }

    public void OnClickExitUI() // UI �ݱ� ��ư Ŭ��
    {
        // UI ��Ȱ��ȭ
        SetGame();
        ResumeGame();
    }

    private void StopGame() // ���� �Ͻ�����, ������ UI Ȱ��ȭ���� �� ���
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()   // ���� �簳, ������ UI �ݾ��� �� ���
    {
        Time.timeScale = 1;
    }

    public void SetGame()   // ������ UI ����
    {
        // �Ͻ������� �޴� UI ����
    }

    public void SetGameOver()   // ���� ������ UI ����
    {
        // �Ͻ����� �� ���ӿ��� UI ����
        StopGame();
        // �����ߴٴ� UI ���(������ �� ����?, Ȩ���� ���ư��� ��ư)
    }

    public void SetClearStage() // �������� Ŭ���� �� UI ����
    {
        // �������� Ŭ�����ߴٴ� ������ ����
    }
}
