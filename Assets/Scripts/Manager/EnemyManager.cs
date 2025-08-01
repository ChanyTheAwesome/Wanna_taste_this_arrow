using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;  // Enemy ���翩�� Ȯ�ο� ���̾�
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    private List<EnemyController> _activeEnemies = new List<EnemyController>();

    private bool _enemySpawnComplete;

    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        SpawnMonster(DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].EnemyCount);
    }

    public void SpawnMonster(int enemyCount)  // �Ϲ� ���� ����
    {
        for(int i = 0; i < enemyCount; i++)
        {
            SpawnRandomMonster();
        }
    }

    public void SpawnBossMonster()  // ���� ���� ����
    {

    }

    public bool CheckEnemyExist()   // Enemy ���̾ ���� ������Ʈ�� �����ϴ��� üũ, ������ false ������ true
    {
        Debug.Log(_activeEnemies.Count.ToString()); // �׽�Ʈ��
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
        //if (/*enemySpawnComplite && */_activeEnemies.Count == 0)
            //gameManager.EndOfWave();
            // ���� ���ٸ� Ŭ���� �����ϱ�

    }
}
