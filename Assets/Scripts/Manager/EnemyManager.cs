using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;  // Enemy 존재여부 확인용 레이어
    [SerializeField] private List<GameObject> enemyPrefabs; // 보스몬스터 리스트 추가
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
        if(!DungeonManager.Instance.CheckBossStage())   // 보스 스테이지가 아니면
            StartCoroutine(SpawnMonster(DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].EnemyCount));
        else    // 보스 스테이지면
        {
            SpawnBossMonster();
        }
    }

    public IEnumerator SpawnMonster(int enemyCount)  // 일반 몬스터 생성
    {
        yield return new WaitUntil(() => PlayerInit.isSetPlayer);
        for(int i = 0; i < enemyCount; i++)
        {
            SpawnRandomMonster();
        }
    }

    public void SpawnBossMonster()  // 보스 몬스터 생성
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
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

    public bool CheckEnemyExist()   // Enemy 레이어를 가진 오브젝트가 존재하는지 체크, 없으면 false 있으면 true
    {
        if (_activeEnemies.Count == 0) return false;
        else return true;
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
        enemyController.Init(this, _playerController.transform); // 에너미컨트롤러의 타겟을 플레이어로 지정하는 코드, 나중에 게임매니저에 플레이어 객체 만들면 다시 넣기

        if (!DungeonManager.Instance.IsFirstStage && !DungeonManager.Instance.CheckBossStage()) // 첫 스테이지, 마지막 보스 스테이지가 아니라면
        {
            for(int i = 0; i < GameManager.Instance.StageCount - 1; i++)    // 스테이지 카운트만큼 스탯 늘리기
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

    public void RemoveEnemyOnDeath(EnemyController enemy)   // _activeEnemies 리스트에서 enemy 없애기, 몬스터 죽는 시점에 실행하기
    {
        _activeEnemies.Remove(enemy);
        DungeonManager.Instance.CheckClearStage();
        //if (/*enemySpawnComplite && */_activeEnemies.Count == 0)
            //gameManager.EndOfWave();
            // 적이 없다면 클리어 세팅하기

    }
}
