using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;  // Enemy ���翩�� Ȯ�ο� ���̾�
    [SerializeField] private List<GameObject> enemyPrefabs; // �������� ����Ʈ �߰�
    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    private List<EnemyController> _activeEnemies = new List<EnemyController>();
    private BossController _bossController;

    //private bool _enemySpawnComplete;

    public static PlayerController _playerController;

    private void Awake()
    {
        if(DungeonManager.Instance.EnemyManager == null)
        {
            DungeonManager.Instance.EnemyManager = this;
        }
    }

    private void Start()
    {
        //StartCoroutine("SpawnMonster", DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].EnemyCount);
        if(!DungeonManager.Instance.CheckBossStage())   // ���� ���������� �ƴϸ�
            StartCoroutine(SpawnMonster(DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].EnemyCount));
        else    // ���� ����������
        {
            SpawnBossMonster();
        }
    }

    public IEnumerator SpawnMonster(int enemyCount)  // �Ϲ� ���� ����
    {
        yield return new WaitUntil(() => PlayerInit.isSetPlayer);
        for(int i = 0; i < enemyCount; i++)
        {
            SpawnRandomMonster();
        }
    }

    public void SpawnBossMonster()  // ���� ���� ����
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        GameObject bossPrefab = enemyPrefabs[0];

        Vector2 spawnPosition = new Vector2(spawnAreas[0].position.x, spawnAreas[0].position.y);

        GameObject spawnBoss = Instantiate(bossPrefab, new Vector3(spawnPosition.x, spawnPosition.y), Quaternion.identity);
        BossController bossController = spawnBoss.GetComponent<BossController>();
        bossController.Init(_playerController.transform);

        //if(bossController != null)
        //{
        //    _bossController = bossController;
        //}
    }

    public bool CheckEnemyExist()   // Enemy ���̾ ���� ������Ʈ�� �����ϴ��� üũ, ������ false ������ true
    {
        if (_activeEnemies.Count == 0) return false;
        else return true;
    }

    private void SpawnRandomMonster()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];    // ������ ���͸� �������� �����ϴ� ����

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];    // spawnArea�� ������ �������� �� �������� �����ϴ� ����

        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));

        GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
        enemyController.Init(this, _playerController.transform); // ���ʹ���Ʈ�ѷ��� Ÿ���� �÷��̾�� �����ϴ� �ڵ�, ���߿� ���ӸŴ����� �÷��̾� ��ü ����� �ٽ� �ֱ�

        if (!DungeonManager.Instance.IsFirstStage && !DungeonManager.Instance.CheckBossStage()) // ù ��������, ������ ���� ���������� �ƴ϶��
        {
            for(int i = 0; i < GameManager.Instance.StageCount - 1; i++)    // �������� ī��Ʈ��ŭ ���� �ø���
            {
                enemyController.SetEnemyHealth(DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].IncreaseStat);
            }
        }
        if (enemyController != null)
        {
            _activeEnemies.Add(enemyController);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)   // _activeEnemies ����Ʈ���� enemy ���ֱ�, ���� �״� ������ �����ϱ�
    {
        _activeEnemies.Remove(enemy);
        DungeonManager.Instance.CheckClearStage();
        //if (/*enemySpawnComplite && */_activeEnemies.Count == 0)
            //gameManager.EndOfWave();
            // ���� ���ٸ� Ŭ���� �����ϱ�

    }
}
