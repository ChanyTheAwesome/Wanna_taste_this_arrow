using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //private Coroutine waveRoutine;

    [SerializeField] private LayerMask enemyLayerMask;  // Enemy 존재여부 확인용 레이어
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    private List<EnemyController> _activeEnemies = new List<EnemyController>();

    private bool _enemySpawnComplete;

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
    private void Start()
    {
        SpawnMonster(DungeonManager.Instance.DungeonList.Find(d => d.ID == DungeonManager.Instance.CurrentDungeonID).EnemyCount);
        // 지금 위에 괄호안에 있는 부분이 잘못됨 아래에 그냥 숫자 넣으면 작동함
        //SpawnMonster(5);
    }

    public void OnClickSpawnMonster()
    {
        SpawnMonster(DungeonManager.Instance.DungeonList.Find(d => d.ID == DungeonManager.Instance.CurrentDungeonID).EnemyCount);
    }

    public void SpawnMonster(int enemyCount)  // 일반 몬스터 생성
    {
        for(int i = 0; i < enemyCount; i++)
        {
            SpawnRandomMonster();
        }
    }

    public void SpawnBossMonster()  // 보스 몬스터 생성
    {

    }

    public bool CheckEnemyExist()   // Enemy 레이어를 가진 오브젝트가 존재하는지 체크, 없으면 false 있으면 true
    {
        Debug.Log(GameManager.Instance.CheckLayerObjectCount(enemyLayerMask));
        if(GameManager.Instance.CheckLayerObjectCount(enemyLayerMask) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SpawnRandomMonster()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];    // 스폰할 몬스터를 랜덤으로 지정하는 로직

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];    // spawnArea를 여러개 설정했을 때 랜덤으로 지정하는 로직

        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));

        GameObject spawnEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyController enemyController = spawnEnemy.GetComponent<EnemyController>();
        //enemyController.Init(this, gameManager.player.transform); << 얘 뭔지 확인해야됨

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

    //public void RemoveEnemyOnDeath(EnemyController enemy)
    //{
    //    activeEnemies.Remove(enemy);
    //    if (enemySpawnComplite && activeEnemies.Count == 0)
    //        gameManager.EndOfWave();
    //}
}
