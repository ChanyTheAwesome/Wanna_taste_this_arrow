using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    //protected UIManager uiManager;
    public virtual void Init(/*UIManager uiManager*/)
    {
        //this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
    protected void StopGame() // 게임 일시정지, 게임중 UI 활성화했을 때 사용
    {
        Time.timeScale = 0;
    }

    protected void ResumeGame()   // 게임 재개, 게임중 UI 닫았을 때 사용
    {
        Time.timeScale = 1;
    }
}
