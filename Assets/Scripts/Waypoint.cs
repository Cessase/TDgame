using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private bool isPlaceable;

    private GridManager gridManager;
    private Vector2Int coordinates = new Vector2Int();
    private PathFinding pathFinder;
    public bool IsPlaceable
    {
        get => isPlaceable;
    }

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockedNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = tower.createTower(tower, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockedNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }

    
}
