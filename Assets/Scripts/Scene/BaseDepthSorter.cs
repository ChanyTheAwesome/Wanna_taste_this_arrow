using UnityEngine;
public abstract class BaseDepthSorter : MonoBehaviour
{
    protected SpriteRenderer sr;

    [SerializeField]
    protected int offset = 100;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>(); //It is REALLY Dangerous to use GetComponent() in Awake()!!!
    }

    protected void UpdateSortingOrder()
    {
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y) + offset;
    }
}