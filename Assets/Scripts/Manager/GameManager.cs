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
}
