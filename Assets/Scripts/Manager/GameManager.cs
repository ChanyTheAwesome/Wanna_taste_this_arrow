using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public PlayerController player { get; private set; }
    //private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    //private EnemyManager enemyManager;

    private UIManager uiManager;
    private SceneController sceneController;
    private PlayerManager playerManager;

    public static bool isFirstLoading = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //player = FindObjectOfType<PlayerController>();
        //player.Init(this);

        uiManager = FindObjectOfType<UIManager>();
        sceneController = FindObjectOfType<SceneController>();
        playerManager = FindObjectOfType<PlayerManager>();

        //enemyManager = GetComponentInChildren<EnemyManager>();
        //enemyManager.Init(this);

        //_playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }
    // Start is called before the first frame update
    private void Start()
    {
        //if (!isFirstLoading)
        //{
        //    StartGame();
        //}
        //else
        //{
        //    isFirstLoading = false;
        //}
    }

    public void StartGame()
    {
        //uiManager.SetPlayGame();
        //StartNextWave();
    }

    void StartNextWave()
    {
        //currentWaveIndex += 1;
        //enemyManager.StartWave(1 + currentWaveIndex / 5);
        //uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfStage()
    {
        //StartNextWave();
    }

    public void GameOver()
    {
        //enemyManager.StopWave();
        //uiManager.SetGameOver();
    }
}
