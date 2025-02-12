using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static List<Node> allNodes = new List<Node>();
    private static Dictionary<Vector2Int, List<Node>> spatialGrid = new Dictionary<Vector2Int, List<Node>>();
    private static float cellSize = 1.2f; // Same as connection radius for optimal partitioning

    public Node cameFrom;
    public List<Node> connections = new List<Node>();
    public float connectionRadius = 1.2f;
    public float gScore;
    public float hScore;
    public float Fscore() => gScore + hScore;

    private Vector2Int gridPosition;
    private static readonly float connectionRadiusSquared = 1.2f * 1.2f;

    private void Awake()
    {
        allNodes.Add(this);
        AddToSpatialGrid();
    }

    private void AddToSpatialGrid()
    {
        gridPosition = WorldToGrid(transform.position);
        if (!spatialGrid.ContainsKey(gridPosition))
        {
            spatialGrid[gridPosition] = new List<Node>();
        }
        spatialGrid[gridPosition].Add(this);
    }

    private void Start()
    {
        StartCoroutine(AddNodesWithinRangeCoroutine());
    }

    private void OnDestroy()
    {
        allNodes.Remove(this);
        if (spatialGrid.ContainsKey(gridPosition))
        {
            spatialGrid[gridPosition].Remove(this);
            if (spatialGrid[gridPosition].Count == 0)
            {
                spatialGrid.Remove(gridPosition);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree") || other.CompareTag("ForeGround"))
        {
            gameObject.SetActive(false);
        }
    }

    private Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPosition.x / cellSize),
            Mathf.FloorToInt(worldPosition.y / cellSize)
        );
    }

    private IEnumerator AddNodesWithinRangeCoroutine()
    {
        const int batchSize = 20; // Increased batch size due to optimizations
        int count = 0;

        // Get neighboring cells
        Vector2Int[] neighborOffsets = new Vector2Int[]
        {
            new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
            new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1),
            new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1)
        };

        foreach (Vector2Int offset in neighborOffsets)
        {
            Vector2Int checkPos = gridPosition + offset;
            if (spatialGrid.TryGetValue(checkPos, out List<Node> nodesInCell))
            {
                foreach (Node otherNode in nodesInCell)
                {
                    if (otherNode != this)
                    {
                        float distanceSqr = (transform.position - otherNode.transform.position).sqrMagnitude;
                        if (distanceSqr <= connectionRadiusSquared)
                        {
                            connections.Add(otherNode);
                        }

                        count++;
                        if (count >= batchSize)
                        {
                            count = 0;
                            yield return null;
                        }
                    }
                }
            }
        }
    }

    // Optional: Static method to clear all data (useful for scene changes)
    public static void ClearAllData()
    {
        allNodes.Clear();
        spatialGrid.Clear();
    }
    
}