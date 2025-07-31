using UnityEngine;

public class DynamicDepthSorter : BaseDepthSorter
{
    private float _lastY;

    protected override void Awake()
    {
        base.Awake();
        _lastY = transform.position.y;
        UpdateSortingOrder(); // �ʱ� ����
    }

    private void Update()
    {
        float currentY = transform.position.y;
        if (!Mathf.Approximately(currentY, _lastY))
        {
            _lastY = currentY;
            UpdateSortingOrder();
        }
    }
}