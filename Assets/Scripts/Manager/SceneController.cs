using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance => instance;

    // 오타 방지
    //private string mainSceneName = "ManagerTestScene";
    //private string gameSceneName = "DungeonTestScene";
    //Declaring the field in advance to avoid typos is a great practice. However, consider using a constant instead!
    //Examples:
    private const string MAIN_SCENE_NAME = "ManagerTestScene";
    private const string GAME_SCENE_NAME = "DungeonTestScene";

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

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
}
