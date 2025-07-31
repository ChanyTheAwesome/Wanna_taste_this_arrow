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
    protected void StopGame() // ���� �Ͻ�����, ������ UI Ȱ��ȭ���� �� ���
    {
        Time.timeScale = 0;
    }

    protected void ResumeGame()   // ���� �簳, ������ UI �ݾ��� �� ���
    {
        Time.timeScale = 1;
    }
}
