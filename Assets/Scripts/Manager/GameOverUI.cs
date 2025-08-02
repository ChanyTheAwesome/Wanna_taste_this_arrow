using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Image dummyGameOverImage;   // ���ӿ����ÿ� ��� UIâ�� �׽�Ʈ�� ���� �̹���
    [SerializeField] private Text clearStageScore;
    [SerializeField] private Text levelUpScore;
    private void Awake()
    {
        UIManager.Instance.GameOverUI = this;
        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        dummyGameOverImage.gameObject.SetActive(false);
        exitDungeonButton.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public void SetGameOverUI()
    {
        StopGame(); // �ð� ����
        dummyGameOverImage.gameObject.SetActive(true);    // �̹��� Ȱ��ȭ
        clearStageScore.text = GameManager.Instance.StageCount.ToString();
        levelUpScore.text = PlayerManager.Instance.Level.ToString();
    }

    public void OnClickExitDungeonButton()  // ���� ������ �޼���
    {
        Time.timeScale = 1;
        DungeonManager.Instance.ExitDungeon();
    }


}
