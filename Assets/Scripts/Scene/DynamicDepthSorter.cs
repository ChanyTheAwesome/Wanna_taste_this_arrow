using UnityEngine;

public class DynamicDepthSorter : BaseDepthSorter
{
    private float lastY;

    protected override void Awake()
    {
        base.Awake();
        lastY = transform.position.y;
        UpdateSortingOrder(); // �ʱ� ����
    }

    private void Update()
    {
        float currentY = transform.position.y;
        if (!Mathf.Approximately(currentY, lastY))
        {
            lastY = currentY;
            UpdateSortingOrder();
        }
    }
}
