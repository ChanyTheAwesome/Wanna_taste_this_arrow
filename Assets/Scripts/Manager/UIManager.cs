using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI ���� enum, Ȩ, ������, �������� ������ ���� ���� < �ʿ��Ѱ� �𸣰���
{

}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneController.instance.LoadGameScene();
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // ������� �ð� �ٽ� ����
        // Ȩ �� �ҷ�����
        DungeonManager.instance.ExitDungeon();
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

    void StopGame() // ���� �Ͻ�����, ������ UI Ȱ��ȭ���� �� ���
    {
        Time.timeScale = 0;
    }

    void ResumeGame()   // ���� �簳, ������ UI �ݾ��� �� ���
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
