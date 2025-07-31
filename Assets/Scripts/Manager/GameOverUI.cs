using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button _exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Image _dummyResultImage;   // ���ӿ����ÿ� ��� UIâ�� �׽�Ʈ�� ���� �̹���

    public override void Init()
    {
        base.Init();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
