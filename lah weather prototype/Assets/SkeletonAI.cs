using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : Health
{
    NavMeshAgent navMesh;

    Camera cam;
    Vector3 hitPos;

    private void Awake()
    {
        cam = Camera.main;
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay,out RaycastHit hit, 100))
            {
                navMesh.SetDestination(hit.point);
                hitPos = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitPos,1);
    }
}
