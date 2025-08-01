using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance => instance;

    // 오타 방지
    private const string MAIN_SCENE_NAME = "MainTitleTest";
    private const string FIRST_DUNGEON_SCENE_NAME = "ForestStageTest";
    private const string FIRST_BOSS_SCENE_NAME = "ForestBossStageTest";
    private const string SECOND_DUNGEON_SCENE_NAME = "CaveStageTest";
    private const string SECOND_BOSS_SCENE_NAME = "CaveBossStageTest";
    private const string THIRD_DUNGEON_START_SCENE_NAME = "CastleStartStageTest";
    private const string THIRD_DUNGEON_LATE_SCENE_NAME = "CastleLateStageTest";
    private const string THIRD_BOSS_SCENE_NAME = "None";

    private int _thirdDungeonStageCount = 5;    // 세번째 던전 전반부, 후반부 나누는 스테이지 수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }

    public void LoadDungeonScene()
    {
        switch (DungeonManager.Instance.CurrentDungeonID)
        {
            case 1:
                SceneManager.LoadScene(FIRST_DUNGEON_SCENE_NAME);
                break;
            case 2:
                SceneManager.LoadScene(SECOND_DUNGEON_SCENE_NAME);
                break;
            case 3:
                if(GameManager.Instance.StageCount < _thirdDungeonStageCount)
                {
                    SceneManager.LoadScene(THIRD_DUNGEON_START_SCENE_NAME);
                }
                else
                {
                    SceneManager.LoadScene(THIRD_DUNGEON_LATE_SCENE_NAME);
                }
                break;
            default:
                Debug.LogError("던전 ID를 찾지 못했습니다.");
                break;
        }
    }

    public void LoadBossScene()
    {
        switch (DungeonManager.Instance.CurrentDungeonID)
        {
            case 1:
                SceneManager.LoadScene(FIRST_BOSS_SCENE_NAME);
                break;
            case 2:
                SceneManager.LoadScene(SECOND_BOSS_SCENE_NAME);
                break;
            case 3:
                SceneManager.LoadScene(THIRD_BOSS_SCENE_NAME);
                break;
        }
    }
}
