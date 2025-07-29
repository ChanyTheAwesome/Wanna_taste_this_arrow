using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    static LoadSceneManager instance;
    public static LoadSceneManager Instance
        { get { return instance; } }

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
        SceneManager.LoadScene("MainTitle");
    }
    public void StartForestStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ForestStage");
        //GameManager.StageCount();  <- 대략 스테이지 수 세는 것

    }
    public void StartForestBossStage()
    {
        //SceneManager.LoadScene("ForestBossStage")
    }
}
