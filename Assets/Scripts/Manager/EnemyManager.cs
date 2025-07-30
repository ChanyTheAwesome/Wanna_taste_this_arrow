using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //private Coroutine waveRoutine;

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

    public void SpawnMonster()  // 일반 몬스터 생성
    {

    }

    public void SpawnBossMonster()  // 보스 몬스터 생성
    {

    }

    public int CheckLayerObjectCount(string targetLayerName)    // 특정 레이어를 가진 게임오브젝트의 개수를 카운팅
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();  // 모든 게임오브젝트를 가져옴, 비활성화된 것 제외

        int targetLayerIndex = LayerMask.NameToLayer(targetLayerName);  // 레이어 인덱스 값 저장

        int prefabCount = 0;

        foreach(GameObject gameObject in allGameObjects)    // 게임오브젝트 순회
        {
            if(gameObject.layer == targetLayerIndex)    // 레이어를 비교
            {
                prefabCount++;  // 맞으면 카운트 증가
            }
        }
        return prefabCount;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
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
