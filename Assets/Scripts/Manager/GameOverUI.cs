using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button _exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Image _dummyResultImage;   // 게임오버시에 띄울 UI창의 테스트용 더미 이미지

    public override void Init()
    {
        base.Init();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
