using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MakeObstacle : MonoBehaviour
{
    public GameObject Obstacles;
    public GameObject Obstacle;

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
    public int ObjCount = 100;
    
    public int StageCount = 1;

    void Start()
    {
        for(int i = 0; i < ObjCount; i++)
        {
            SpawnObstacle();
        }
    }

    public void SpawnObstacle() //
    {
        int countX = (int)((maxX - minX) / xMultiplier)+1;
        int countY = (int)((maxY - minY) / yMultiplier)+1;


        int xPosMult = Random.Range(0, countX);
        int yPosMult = Random.Range(0, countY);

        float xPos = xPosMult * xMultiplier + minX;
        float yPos = yPosMult * yMultiplier + minY;

        Vector3 obstaclePos = new Vector3(xPos, yPos, 0);
        Instantiate(Obstacle, obstaclePos, Quaternion.identity, Obstacles.transform);
    }
}