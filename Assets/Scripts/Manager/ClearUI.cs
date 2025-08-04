using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : BaseUI
{
    [SerializeField] private Text stageClearTxt;
    [SerializeField] private Text dungeonClearTxt;

    private void Awake()
    {
        stageClearTxt.gameObject.SetActive(false);
        dungeonClearTxt.gameObject.SetActive(false);
        UIManager.Instance.ClearUI = this;
    }

    public void SetClearUI()
    {
        if (GameManager.Instance.StageCount == DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].MaxStageCount)
        {
            dungeonClearTxt.gameObject.SetActive(true);
        }
        else
        {
            stageClearTxt.gameObject.SetActive(true);
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Clear;
    }
}
