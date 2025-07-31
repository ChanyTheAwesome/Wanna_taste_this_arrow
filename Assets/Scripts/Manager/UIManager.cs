using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI 상태 enum, 홈, 게임중, 게임종료 등으로 나눌 예정 < 필요한가 모르겠음
{

}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnClickMenu()   // 메뉴 버튼 클릭
    {
        StopGame();
        // 메뉴 UI 띄우기
    }

    public void OnClickStop()   // 일시정지 버튼 클릭
    {
        StopGame();
        // 일시정지 UI 띄우기
    }

    public void OnClickStart()  // 시작 버튼 클릭
    {
        // 게임 씬 불러오기
        SceneController.Instance.LoadGameScene();
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // 멈춰놨던 시간 다시 세팅
        // 홈 씬 불러오기
        DungeonManager.Instance.ExitDungeon();
    }

    public void OnClickOption() // 옵션 버튼 클릭
    {
        StopGame();
        // 옵션 UI 띄우기
    }

    public void OnClickExitUI() // UI 닫기 버튼 클릭
    {
        // UI 비활성화
        SetGame();
        ResumeGame();
    }

    private void StopGame() // 게임 일시정지, 게임중 UI 활성화했을 때 사용
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()   // 게임 재개, 게임중 UI 닫았을 때 사용
    {
        Time.timeScale = 1;
    }

    public void SetGame()   // 게임중 UI 설정
    {
        // 일시정지나 메뉴 UI 띄우기
    }

    public void SetGameOver()   // 게임 오버시 UI 설정
    {
        // 일시정지 후 게임오버 UI 띄우기
        StopGame();
        // 실패했다는 UI 출력(진행결과 등 포함?, 홈으로 돌아가기 버튼)
    }

    public void SetClearStage() // 스테이지 클리어 시 UI 설정
    {
        // 스테이지 클리어했다는 문구라도 띄우기
    }
}
