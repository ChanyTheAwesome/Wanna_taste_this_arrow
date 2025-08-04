using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class NavigationSurfaceScript : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;

    private void Start()
    {
        if(surface == null)
        {
            surface = GetComponent<NavMeshSurface>();
        }

        if (surface != null)
        {
            surface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface component is not assigned or found on the GameObject.");
        }
    }
}
