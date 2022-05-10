using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Tiles
{
    public abstract class NodeBase : MonoBehaviour
    {
        [Header("References")]
        private Color _defaultColor;
        [SerializeField] private Color _obstacleColor;
        [SerializeField] private Gradient _walkableColor;
        [SerializeField] protected SpriteRenderer _renderer;
     
        public ICoords Coords;
        public float GetDistance(NodeBase other) => Coords.GetDistance(other.Coords); // Helper to reduce noise in pathfinding
        public bool Walkable { get; private set; }
        private bool _selected;

        public static event Action<NodeBase> OnHoverTile;
        private void OnEnable() => OnHoverTile += OnOnHoverTile;
        private void OnDisable() => OnHoverTile -= OnOnHoverTile;
        private void OnOnHoverTile(NodeBase selected) => _selected = selected == this;

        protected virtual void OnMouseDown()
        {
            if (!Walkable) return;
            OnHoverTile?.Invoke(this);
        }

        public virtual void Init(bool walkable, ICoords coords)
        {
            Walkable = walkable;

            _renderer.color = walkable ? _walkableColor.Evaluate(Random.Range(0f, 1f)) : _obstacleColor;
            _defaultColor = _renderer.color;

            OnHoverTile += OnOnHoverTile;

            Coords = coords;
            transform.position = Coords.Pos;
        }

        #region Pathfinding

        [Header("Pathfinding")]
        [SerializeField] private TextMeshPro _gCostText;
        [SerializeField] private TextMeshPro _hCostText;
        [SerializeField] private TextMeshPro _fCostText;

        public float G { get; private set; }
        public void SetG(float g) { G = g; SetText(); }
        public float H { get; private set; }
        public void SetH(float h) { H = h; SetText(); }
        public float F => G + H;
        
        public List<NodeBase> Neighbors { get; protected set; }
        public abstract void CacheNeighbors();
        
        public NodeBase Connection { get; private set; }
        public void SetConnection(NodeBase nodeBase) { Connection = nodeBase; }

        public void SetColor(Color color) => _renderer.color = color;
        private void SetText()
        {
            if (_selected) return;
            _gCostText.text = G.ToString();
            _hCostText.text = H.ToString();
            _fCostText.text = F.ToString();
        }
        public void RevertTile()
        {
            _renderer.color = _defaultColor;
            _gCostText.text = "";
            _hCostText.text = "";
            _fCostText.text = "";
        }

        #endregion
    }
}


public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}