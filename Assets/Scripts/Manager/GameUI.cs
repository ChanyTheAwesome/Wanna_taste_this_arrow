using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button _menuButton;    // �޴� ��ư
    [SerializeField] private Button _exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Button _closeMenuButton;  // UI �ݱ� ��ư
    [SerializeField] private Image _menuImage; // �޴� ������ �� ��� UI �̹���
    // �޴� ��ư ������ ��

    private void Awake()
    {
        //Init();
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        _closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
    }

    //public override void Init()
    //{
    //    _menuButton.onClick.AddListener(OnClickMenuButton);
    //    _exitDungeonButton.onClick.AddListener(OnClickExitDungeon);
    //    _closeUIButton.onClick.AddListener(OnClickExitUI);
    //}

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void OnClickMenuButton()   // �޴� ��ư Ŭ��
    {
        StopGame(); // ���� ����
        SetMenu();  // �޴�â ����
    }

    public void OnClickExitDungeonButton()
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

    public void OnClickCloseMenuButton() // UI �ݱ� ��ư Ŭ��
    {
        // UI ��Ȱ��ȭ
        SetGame();
        ResumeGame();
    }

    public void SetGame()   // ������ UI ����
    {
        // �޴� UI ����
        _menuButton.interactable = true;
        _menuImage.gameObject.SetActive(false);
    }

    public void SetMenu()
    {
        _menuButton.interactable = false;
        _menuImage.gameObject.SetActive(true);
    }
}
