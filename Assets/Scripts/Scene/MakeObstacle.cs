using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MakeObstacle : MonoBehaviour
{
    public GameObject obstacles;
    public GameObject obstacle;

    [SerializeField]
    private float maxY = 22f;
    [SerializeField]
    private float minY = 0f;
    [SerializeField]
    private float maxX = 7.6f;
    [SerializeField]
    private float minX = -7.6f;
    [SerializeField]
    private float yMultiplier = 0.5f;
    [SerializeField]
    private float xMultiplier = 0.8f;
    [SerializeField]
    public int objCount = 100;
    

    public int stageCount = 1;

    void Start()
    {
        for(int i = 0; i < objCount; i++)
        {

            SpawnObstacle();
        }
    }

    public void SpawnObstacle() //
    {
        int countX = (int)((maxX - minX) / xMultiplier);
        int countY = (int)((maxY - minY) / yMultiplier);


        int xPosMult = Random.Range(0, countX);
        int yPosMult = Random.Range(0, countY);

        float xPos = xPosMult * xMultiplier + minX;
        float yPos = yPosMult * yMultiplier + minY;

        Vector3 obstaclePos = new Vector3(xPos, yPos, 0);
        Instantiate(obstacle, obstaclePos, Quaternion.identity, obstacles.transform);
    }

}
