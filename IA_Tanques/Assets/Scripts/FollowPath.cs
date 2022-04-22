using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed = 5.0f;
    [SerializeField] float accuracy = 1.0f;
    [SerializeField] float rotSpeed = 2.0f;

    [Header("References")]
    public WpManager waypointManager;
    Graph graph;
    GameObject[] waypoints;
    Transform goal;
    GameObject currentNode;
    int currentWaypointIndex = 0;

    void Start()
    {
        waypoints = waypointManager.waypoints;
        graph = waypointManager.graph;
        currentNode = waypoints[0];
    }
    void LateUpdate()
    {
        /*
            Impede que o método rode caso o caminho a ser percorrido não contenha nenhum node
            ou caso o tanque já tenha alcançado o último node.
        */
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength()) return;

        /*
            Inicialmente, 'currentNode' será waypoints[0] e, então,
            passa a ser o nodo mais próximo do tanque.
        */
        currentNode = graph.getPathPoint(currentWaypointIndex);

        /*
            O próximo waypoint do caminho que está seguindo passa a ser o alvo assim que
            o tanque estiver próximo o suficiente do seu alvo atual.
        */
        if(Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position, transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        /*
            Caso o tanque ainda não tenha atingido o último waypoint do caminho que está seguindo
            passa a se mover em direção ao próximo.
        */
        if(currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;

            /*
                Posição em direção a qual o tanque deve se virar.
            */
            Vector3 lookAtGoal = new Vector3
            (
                goal.position.x,
                this.transform.position.y,
                goal.position.z
            );

            /*
                Direção na qual o tanque deve se virar.
            */
            Vector3 direction = lookAtGoal - this.transform.position;
            
            /*
                Rotaciona o tanque na direção determinada.
            */
            this.transform.rotation = Quaternion.Slerp
            (
                this.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed
            );

            /*
                Move o tanque em seu eixo frontal.
            */
            this.transform.Translate
            (
                Vector3.forward * speed,
                Space.Self
            );
        }
    }

    /*
        Substituí os métodos 'GoToHeli' e 'GoToRuin'
        por um único mais flexível, recebe como argumento o node ao qual deve ir.
    */
    public void GoToNode(GameObject targetNode)
    {
        /*
            Tentar ir para o node atual causou um crash na unity...
            O método não permite mais que o alvo seja o mesmo que o node atual.
        */
        if(targetNode == currentNode) return;

        /*
            Gera um novo caminho do node atual até 'targetNode' e inicia a movimentação do tanque por esse caminho.
        */
        graph.AStar(currentNode, targetNode);
        currentWaypointIndex = 0;
    }
}