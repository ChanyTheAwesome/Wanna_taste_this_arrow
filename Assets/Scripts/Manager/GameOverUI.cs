using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Image dummyGameOverImage;   // 게임오버시에 띄울 UI창의 테스트용 더미 이미지

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
        StopGame(); // 시간 정지
        dummyGameOverImage.gameObject.SetActive(true);    // 이미지 활성화
    }

    public void OnClickExitDungeonButton()  // 던전 나가기 메서드
    {
        Time.timeScale = 1;
        DungeonManager.Instance.ExitDungeon();
    }
}
