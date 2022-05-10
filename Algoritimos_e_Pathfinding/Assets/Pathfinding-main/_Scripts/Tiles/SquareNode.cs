using System.Collections.Generic;
using System.Linq;
using Tarodev_Pathfinding._Scripts.Grid;
using UnityEngine;

namespace _Scripts.Tiles
{
    public class SquareNode : NodeBase
    {
        /*
            Direções para as quais o movimento é permitido no grid.
            Cima, baixo, lados e diagonais.
        */
        private static readonly List<Vector2> Dirs = new List<Vector2>()
        {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

        /*
            Lista as tiles vizinhas.
        */
        public override void CacheNeighbors()
        {
            Neighbors = new List<NodeBase>();

            foreach 
            (
                var tile in Dirs.Select // Analiza tiles em cada direção
                (
                    dir => GridManager.Instance.GetTileAtPosition(Coords.Pos + dir)
                ).Where // Adiciona na lista se houver
                (
                    tile => tile != null
                )
            )
            {
                Neighbors.Add(tile);
            }
        }

        /*
            Inicia a tile:
            determina se pode ser atravessada, coordenadas no grid,
            cor, posição, rotação
        */
        public override void Init(bool walkable, ICoords coords)
        {
            base.Init(walkable, coords);
            
            _renderer.transform.rotation = Quaternion.Euler(0, 0, 90 * Random.Range(0, 4));
        }
    }
}

/*
    Struct define sistema de coordenadas num grid de casas quadradas.
*/
public struct SquareCoords : ICoords
{
    /*
        Pega a distância da coordenada atual no grid até uma outra.
    */
    public float GetDistance(ICoords other) 
    {
        var dist = new Vector2Int
        (
            Mathf.Abs((int)Pos.x - (int)other.Pos.x),
            Mathf.Abs((int)Pos.y - (int)other.Pos.y)
        );

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        /*
            Calcula quantos movimentos não diagonais
            irão ser usados na trajetória.
        */
        var horizontalMovesRequired = highest - lowest;

        /*
            Num grid de quadrados os movimentos diagonais cobrem mais distância.
        */
        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
    
    /*
        Posição atual.
    */
    public Vector2 Pos { get; set; }
}
