using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private EnemyManager _enemyManager;

    private int stageCount = 0;
    public int StageCount
    {
        get { return stageCount; }
        set { stageCount = value; }
    }
    // ���� �������� �������� �ѹ�, �������� ���۽ÿ� �ö󰡰� ����

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _enemyManager = FindObjectOfType<EnemyManager>();//<- This is TOO DANGEROUS!
        DungeonManager.Instance.CurrentDungeonID = 1;
    }

    private void Update()
    {
        if (_enemyManager.CheckEnemyExist())
        {
            Debug.Log("�� ����");
        }
        else
        {
            Debug.Log("�� ����");
        }
        DungeonManager.Instance.CheckClearStage();

    }

    public void StartGame() // ó���� ���� �������� �� ������ �͵�, ���������� ���� ���� �ƴ�, ���� 1ȸ�� ����� �ٽ� ���ƿ͵� ����ȵ�
    {
        
    }

    public int CheckLayerObjectCount(LayerMask targetLayer)    // Ư�� ���̾ ���� ������Ʈ�� ������ ī����
    {//Too Inefficient to find Enemies! EnemyManager already has "activeEnemies" List, please consider to use activeEnemies.Count
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();  // ��� ���ӿ�����Ʈ�� ������, ��Ȱ��ȭ�� �� ����

        int objectCount = 0;

        foreach (GameObject gameObject in allGameObjects)    // ���ӿ�����Ʈ ��ȸ
        {
            if ((targetLayer.value & (1 << gameObject.layer)) != 0)    // ���̾ ��
            {
                objectCount++;  // ������ ī��Ʈ ����
            }
        }
        return objectCount;
    }

    public void UpStageCount()
    {
        stageCount++;
        Debug.Log(stageCount);
    }

    public void DownStageCount()
    {
        stageCount--;
        Debug.Log(stageCount);
    }
}
