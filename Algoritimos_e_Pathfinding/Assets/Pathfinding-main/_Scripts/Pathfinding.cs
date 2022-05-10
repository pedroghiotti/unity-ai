using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts
{
    /// <summary>
    /// This algorithm is written for readability. Although it would be perfectly fine in 80% of games, please
    /// don't use this in an RTS without first applying some optimization mentioned in the video: https://youtu.be/i0x5fj4PqP4
    /// If you enjoyed the explanation, be sure to subscribe!
    ///
    /// Also, setting colors and text on each hex affects performance, so removing that will also improve it marginally.
    /// </summary>
    public static class Pathfinding
    {
        /*
            Cores pra representação visual do pathfinding.
        */
        private static readonly Color PathColor = new Color(0.65f, 0.35f, 0.35f); // Nodes no caminho encontrado.
        private static readonly Color OpenColor = new Color(.4f, .6f, .4f); // Nodes esperando verificação.
        private static readonly Color ClosedColor = new Color(0.35f, 0.4f, 0.5f); // Nodes já verificados.
        
        public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
        {
            /*
                Lista dos nodes esperando verificação.
                Inicialmente contém apenas o 'startNode'.
            */
            var toSearch = new List<NodeBase>() { startNode };
            var processed = new List<NodeBase>(); // Lista dos nodes já verificados.

            /*
                * No primeiro loop verifica apenas o 'startNode',
                então vai adicionando os nodes vizinhos.

                Enquanto ainda houver nodes esperando verificação:
            */
            while (toSearch.Any())
            {
                var current = toSearch[0];

                /*
                    Itera pelos nodes na lista 'toSearch', procurando pelo mais próximo ao node alvo.
                    Para avaliar o mais apropriado, utiliza os valores G, H e F do node.
                */
                foreach (var t in toSearch)
                {
                    if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;
                }
                Debug.Log(current.H);
                /*
                    Adiciona a lista dos nodes já verificados.
                    Retira da lista dos nodes esperando verificação.
                */
                processed.Add(current);
                toSearch.Remove(current);
                
                current.SetColor(ClosedColor);

                /*
                    Ao encontrar o node alvo constrói o caminho.
                */
                if (current == targetNode)
                {
                    var currentPathTile = targetNode;
                    var path = new List<NodeBase>();
                    var count = 100;
                    while (currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                        Debug.Log("sdfsdf");
                    }
                    
                    foreach (var tile in path) tile.SetColor(PathColor);
                    startNode.SetColor(PathColor);
                    Debug.Log(path.Count);
                    return path;
                }

                /*
                    Itera pelos nodes vizinhos ainda não analizados e que podem ser atravessados.
                    Calcula seus valores G, H e F.
                    Caso ainda não estejam na lista 'toSearch', adiciona.
                    Caso estejam, atualiza valores.
                */
                foreach
                (
                    var neighbor in current.Neighbors.Where
                    (
                        t => t.Walkable == true && 
                        processed.Contains(t) == false
                    )
                )
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetDistance(neighbor);

                    if (inSearch == false || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (inSearch == false)
                        {
                            neighbor.SetH(neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                            neighbor.SetColor(OpenColor);
                        }
                    }
                }
            }
            return null;
        }
    }
}