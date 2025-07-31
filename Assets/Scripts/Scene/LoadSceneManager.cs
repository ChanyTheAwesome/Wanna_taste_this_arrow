using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager instance;
    public static LoadSceneManager Instance
        { get { return instance; } }

    private const string MAIN_TITLE_NAME = "MainTitle";
    private const string FOREST_STAGE_NAME = "ForestStage";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GoMainTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MAIN_TITLE_NAME);
    }
    public void StartForestStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(FOREST_STAGE_NAME);
        //GameManager.StageCount();  <- 대략 스테이지 수 세는 것
    }
    public void StartForestBossStage()
    {
        //SceneManager.LoadScene("ForestBossStage")
    }
}
