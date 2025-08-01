using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button _menuButton;    // �޴� ��ư
    [SerializeField] private Button _exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Button _exitUIButton;  // UI �ݱ� ��ư
    [SerializeField] private Image _dummyImage; // �޴� ������ �� ��� UI�� �׽�Ʈ�� ����
    // �޴� ��ư ������ ��

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _exitDungeonButton.onClick.AddListener(OnClickExitDungeon);
        _exitUIButton.onClick.AddListener(OnClickExitUI);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void OnClickMenuButton()   // �޴� ��ư Ŭ��
    {
        StopGame(); // ���� ����
        SetMenu();  // �޴�â ����
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // ������� �ð� �ٽ� ����
        // Ȩ �� �ҷ�����
        DungeonManager.Instance.ExitDungeon();
    }

    //public void OnClickOption() // �ɼ� ��ư Ŭ��
    //{
    //    StopGame();
    //    // �ɼ� UI ����
    //}

    public void OnClickExitUI() // UI �ݱ� ��ư Ŭ��
    {
        // UI ��Ȱ��ȭ
        SetGame();
        ResumeGame();
    }

    public void SetGame()   // ������ UI ����
    {
        // �޴� UI ����
        _menuButton.gameObject.SetActive(true);
        _dummyImage.gameObject.SetActive(false);
    }

    public void SetMenu()
    {
        _menuButton.gameObject.SetActive(false);
        _dummyImage.gameObject.SetActive(true);
    }
}
