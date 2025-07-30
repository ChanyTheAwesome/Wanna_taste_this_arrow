using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int stageCount = 0;  // ���� �������� �������� �ѹ�, �������� ���۽ÿ� �ö󰡰� ����

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
    }
    // Start is called before the first frame update
    private void Start()
    {
        StartGame();    // ���� �Ŵ����� �ı����� �ʰ� �ߴµ� ���� �ٽ� �ҷ��͵� Start�� �����ϳ�?
    }

    public void StartGame() // ó���� ���� �������� �� ������ �͵�, ���������� ���� ���� �ƴ�, ���� 1ȸ�� ����� �ٽ� ���ƿ͵� ����ȵ�
    {
        
    }

    public int CheckLayerObjectCount(LayerMask targetLayer)    // Ư�� ���̾ ���� ������Ʈ�� ������ ī����
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();  // ��� ���ӿ�����Ʈ�� ������, ��Ȱ��ȭ�� �� ����

        int objectCount = 0;

        foreach (GameObject gameObject in allGameObjects)    // ���ӿ�����Ʈ ��ȸ
        {
            if (gameObject.layer == targetLayer)    // ���̾ ��
            {
                objectCount++;  // ������ ī��Ʈ ����
            }
        }
        return objectCount;
    }
}
