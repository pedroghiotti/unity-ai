using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshMovementController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }
    private void Update()
    {
        /*
            Faz um Raycast a partir da câmera em direção ao cursor.
            Coloca o ponto de colisão desse raycast como ponto alvo para o NavMeshAgent.
        */
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray;
            RaycastHit raycastHitInfo;
            Vector3 targetPos;
    
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHitInfo);
            targetPos = raycastHitInfo.point;

            navMeshAgent.SetDestination(targetPos);
        }
    }
}
