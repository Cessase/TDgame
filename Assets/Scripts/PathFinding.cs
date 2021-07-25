using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Vector2Int StartCoordinates => startCoordinates;
    public Vector2Int DestinationCoordiates => destinationCoordiates;

    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordiates;
    [SerializeField] private Node currentSearchNode;

    private Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    private Node startNode;
    private Node destinationNode;

    private Queue<Node> frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> grid;
    private Dictionary<Vector2Int, Node> reachedGrid = new Dictionary<Vector2Int, Node>();
    private GridManager gridManager;
    private List<Node> path;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = gridManager.Grid[startCoordinates];
            destinationNode = gridManager.Grid[destinationCoordiates];
        }
    }
    
    void Start()
    {
        GetNewPath();
    }

    void ExploreNeighboors()
    {
        List<Node> neighboors = new List<Node>();

        foreach (var direction in directions)
        {
            Vector2Int neighboorCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighboorCoords))
            {
                neighboors.Add(grid[neighboorCoords]);
            }
        }

        foreach (var neighboor in neighboors)
        {
            if (!reachedGrid.ContainsKey(neighboor.coordinates) && neighboor.isWalkable)
            {
                neighboor.connectedTo = currentSearchNode;
                reachedGrid.Add(neighboor.coordinates,neighboor);
                frontier.Enqueue(neighboor);
            }
        }
    }

    

    void BreadFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        
        frontier.Clear();
        reachedGrid.Clear();
        
        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reachedGrid.Add(coordinates,grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighboors();
            if (currentSearchNode.coordinates == destinationCoordiates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        path = new List<Node>();
        Node currentNode = destinationNode;
        
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            
            path.Add(currentNode);
            currentNode.isPath = true;
            if (currentNode == startNode)
            {
                break;
            }
        }

        path.Reverse();
        return path;
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadFirstSearch(coordinates);
        BuildPath();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false ,SendMessageOptions.DontRequireReceiver);
    }
}
