using UnityEngine;

public class StaticDepthSorter : BaseDepthSorter
{
    private void Start()
    {
        UpdateSortingOrder(); // 한 번만 정렬
    }
}