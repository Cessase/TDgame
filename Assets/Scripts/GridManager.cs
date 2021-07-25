using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [Tooltip("World Grid size - Should match Unity editor snap settings")]
    [SerializeField] private int unityGridSize = 1;
    public int UnityGridSize => unityGridSize;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get => grid; set => grid = value; }


    private void Awake()
    {
        CreateGrid();
    }

    public void BlockedNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
        
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);
        return coordinates;
    }
    
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnityGridSize;
        position.z = coordinates.y * UnityGridSize;
        return position;
    }

    void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coordinates = new Vector2Int(i, j);
                grid.Add(coordinates, new Node(coordinates, true));
                //Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }
    
    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
        
    }
}
