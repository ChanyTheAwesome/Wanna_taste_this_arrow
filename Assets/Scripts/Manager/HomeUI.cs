using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button _shiftLeftButton;
    [SerializeField] private Button _shiftRightButton;
    [SerializeField] private Button _enterDungeonButton;
    [SerializeField] private Text _currentDungeonIDText;

    private void Update()
    {
        _currentDungeonIDText.text = DungeonManager.Instance.CurrentDungeonID.ToString();
        Debug.Log(DungeonManager.Instance.CurrentDungeonID);
    }

    public override void Init(/*UIManager uiManager*/)
    {
        //base.Init(uiManager);

        _shiftLeftButton.onClick.AddListener(OnClickShiftLeftButton);
        _shiftRightButton.onClick.AddListener(OnClickShiftRightButton);
        _enterDungeonButton.onClick.AddListener(OnClickEnterDungeonButton);
    }

    public void OnClickShiftLeftButton()
    {
        if (DungeonManager.Instance.CurrentDungeonID > 1)
        {
            DungeonManager.Instance.CurrentDungeonID--;
            // 던전 이미지나 이름도 바꾸기
        }
        else return;
    }

    public void OnClickShiftRightButton()
    {
        if (DungeonManager.Instance.CurrentDungeonID < DungeonManager.Instance.DungeonDict.Count)
        {
            DungeonManager.Instance.CurrentDungeonID++;
            // 던전 이미지나 이름도 바꾸기
        }
        else return;
    }

    public void OnClickEnterDungeonButton()
    {
        DungeonManager.Instance.StartDungeon(DungeonManager.Instance.CurrentDungeonID);
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}
