using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState // UI ���� enum, Ȩ, ������, �������� ������ ���� ���� < �ʿ��Ѱ� �𸣰���
{

}

public class UIManager : MonoBehaviour
{
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
        //SceneManager.LoadScene("GameScene");
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // ������� �ð� �ٽ� ����
        // �÷��̾� ���� 1�� �����
        // �÷��̾� ����ġ 0���� �����
        // PlayerManager.ResetLevel();
        // Ȩ �� �ҷ�����
        // SceneManager.LoadScene("MainScene");
        // stageCount �ٽ� 0���� ���߱�
        GameManager.instance.stageCount = 0;
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
        // �����ߴٴ� UI ���(������ �� ����?)
        // ���� ȭ������ ���ư��� ��ư
        // OnClickExitDungeon();
    }

    public void SetClearStage() // �������� Ŭ���� �� UI ����
    {
        // �������� Ŭ�����ߴٴ� ������ ����
    }
}
