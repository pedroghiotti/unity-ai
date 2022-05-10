using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Estrutura da ligação entre dois nodes.
*/
[System.Serializable]
public struct Link
{
    public enum direction {UNI, BI}
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WpManager : MonoBehaviour
{
    [Header("References")]
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {
        /*
            Impede que o método rode se não houver nenhum waypoint.
        */
        if(waypoints.Length <= 0) return;

        foreach(GameObject waypoint in waypoints)
        {
            graph.AddNode(waypoint);
        }

        foreach(Link link in links)
        {
            graph.AddEdge(link.node1, link.node2);

            if (link.dir == Link.direction.BI)
            {
                graph.AddEdge(link.node2, link.node1);
            }
        }
    }
    
    void Update()
    {
        graph.debugDraw();
    }
}
