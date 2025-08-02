using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Image dummyGameOverImage;   // ���ӿ����ÿ� ��� UIâ�� �׽�Ʈ�� ���� �̹���

    private void Awake()
    {
        UIManager.Instance.GameOverUI = this;
        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        dummyGameOverImage.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public void SetGameOverUI()
    {
        StopGame(); // �ð� ����
        dummyGameOverImage.gameObject.SetActive(true);    // �̹��� Ȱ��ȭ
    }

    public void OnClickExitDungeonButton()  // ���� ������ �޼���
    {
        Time.timeScale = 1;
        DungeonManager.Instance.ExitDungeon();
    }
}
