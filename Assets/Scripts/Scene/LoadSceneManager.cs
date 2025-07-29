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

    public void StartForestStage()
    {
        SceneManager.LoadScene("ForestStage");
    }
}
