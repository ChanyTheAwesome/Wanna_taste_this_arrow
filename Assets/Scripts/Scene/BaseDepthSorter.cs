using UnityEngine;
public abstract class BaseDepthSorter : MonoBehaviour
{
    protected SpriteRenderer sr;

    [SerializeField]
    protected int offset = 100;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected void UpdateSortingOrder()
    {
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y) + offset;
    }
}
