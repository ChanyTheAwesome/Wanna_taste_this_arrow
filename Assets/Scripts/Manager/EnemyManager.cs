using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //private Coroutine waveRoutine;

    [SerializeField] private LayerMask enemyLayerMask;  // Enemy ���翩�� Ȯ�ο� ���̾�

    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    //private List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool enemySpawnComplite;

    //[SerializeField] private float timeBetweenSpawns = 0.2f;
    //[SerializeField] private float timeBetweenWaves = 1f;

    //GameManager gameManager;

    //public void Init(GameManager gameManager)
    //{
    //    this.gameManager = gameManager;
    //}



    //private IEnumerator SpawnWave(int waveCount)
    //{
    //    enemySpawnComplite = false;
    //    yield return new WaitForSeconds(timeBetweenWaves);

    //    for (int i = 0; i < waveCount; i++)
    //    {
    //        yield return new WaitForSeconds(timeBetweenSpawns);
    //        SpawnRandomEnemy();
    //    }

    //    enemySpawnComplite = true;
    //}

    public void SpawnMonster()  // �Ϲ� ���� ����
    {

    }

    public void SpawnBossMonster()  // ���� ���� ����
    {

    }

    public bool CheckEnemyExist()   // Enemy ���̾ ���� ������Ʈ�� �����ϴ��� üũ, ������ false ������ true
    {
        Debug.Log(GameManager.instance.CheckLayerObjectCount(enemyLayerMask));
        if(GameManager.instance.CheckLayerObjectCount(enemyLayerMask) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));

        //GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        //EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
        //enemyController.Init(this, gameManager.player.transform);

        //activeEnemies.Add(enemyController);
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

    //public void RemoveEnemyOnDeath(EnemyController enemy)
    //{
    //    activeEnemies.Remove(enemy);
    //    if (enemySpawnComplite && activeEnemies.Count == 0)
    //        gameManager.EndOfWave();
    //}
}
