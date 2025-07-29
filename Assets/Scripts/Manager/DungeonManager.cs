using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList;

    public bool isClear = false;

    public bool isEnd = false;  // ���� ������ �� �� ��Ҵ��� ����, true�� �ٲٸ� �ٷ� ���� �������� �̵�

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dungeonList.Clear();
        dungeonList.Add(new Dungeon(1, "1�ܰ� ����", 10, 6));
        dungeonList.Add(new Dungeon(2, "2�ܰ� ����", 10, 6));
        dungeonList.Add(new Dungeon(3, "3�ܰ� ����", 10, 6));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClearStage()    // �������� Ŭ����
    {
        // ���ʹ� �ִ��� Ȯ��

        // ���������� ���ư���
        // ������ Ŭ����
        ClearStage();
    }

    public void ClearStage()
    {
        isClear = true;
        // �� ó���ߴٴ� UI ����?
    }

    public void StartDungeon(Dungeon dungeon)
    {
        StartStage(dungeon.StageCount);
    }

    public IEnumerable StartStage(int stageCount)
    {
        // UIState �������� �����ϱ�
        // �������� �ݺ� > �ݺ��� �ؼ� ������ ���������� ������
        for(int i = 1; i <= stageCount; i++)    // �ڷ�ƾ ��߰ڴ� yield return new WaitUntil(bool predicate) << �̿��ϸ� �ɵ�
        {
            // �÷��̾� ��ġ ����

            // ���� ����
            if(i == stageCount) // ������ ���������� ��
            {
                // ���� �������� ����
            }
            else    // ������ �ƴҶ�
            {
                // �� ��ֹ� ���ġ
                // �Ϲ� ���� ��ġ
            }
            yield return new WaitUntil(() => isEnd);  // �ڷ�ƾ ����ؼ� ����ϱ�, ���� �������� �Ѿ���� isEnd true�� �ٲ��ֱ�
        }
        
        // ������ ������ << ��� üũ�ؾߵǴ� update��? �ƴϸ� �� ���϶� üũ�ϴ� �޼��� ����� �߰�?
        // ���ʹ� �ʿ��� ���� ������ �޾��� �� ü���� �����ϴ� �������� �״��� Ȯ��
    }
}
