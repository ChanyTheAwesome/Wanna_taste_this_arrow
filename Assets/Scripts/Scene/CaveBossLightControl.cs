using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBossLightControl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> alleyObstacleList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> hallObstacleList = new List<GameObject>();

    [SerializeField]
    private int alleyColumns = 2;
    [SerializeField]
    private int hallColumns = 2;

    private GameObject player;
    private int alleyObstacleRowLength;
    private int hallObstacleRowLength;

    void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject;
        }
        else
        {
            Debug.Log("not found player");
        }

        // y 값 기준 오름차순 정렬
        alleyObstacleList.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        hallObstacleList.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        alleyObstacleRowLength = Mathf.CeilToInt((float)alleyObstacleList.Count / alleyColumns);
        hallObstacleRowLength = Mathf.CeilToInt((float)hallObstacleList.Count / hallColumns);
    }

    void Update()
    {
        CheckAndActivateLights();
    }

    private void CheckAndActivateLights()
    {
        
        float playerY = player.transform.position.y;

        for (int i = 0; i < alleyObstacleRowLength; i++)
        {
            float obstacleGroupY = alleyObstacleList[i * alleyColumns].transform.position.y;
            if (playerY >= obstacleGroupY)
            {
                ActivateLightPair(i * alleyColumns);
            }
        }

        if (playerY >= hallObstacleList[0].transform.position.y - 1f)
        {
            StartCoroutine(ActivateLightBoss());
        }
    }

    private void ActivateLightPair(int pairIndex)
    {
        for (int i = pairIndex; i < pairIndex + alleyColumns && i < alleyObstacleList.Count; i++)
        {
            alleyObstacleList[i].SetActive(true);
        }
    }

    IEnumerator ActivateLightBoss()
    {
        yield return new WaitForSeconds(0);

        for (int i = 0; i < hallObstacleRowLength; i++)
        {
            int start = i * hallColumns;
            for (int j = start; j < start + hallColumns && j < hallObstacleList.Count; j++)
            {
                hallObstacleList[j].SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(3f);
        GetComponent<CaveBossLightControl>().enabled = false;
    }
}
