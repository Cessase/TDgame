using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Coordinate : MonoBehaviour
{
    // Start is called before the first frame update
     private Color defaultColor = Color.white;
     private Color blockedColor = Color.gray;
     private Color exploredColor = Color.yellow;
     private Color pathColor = new Color(1f,0.5f,0f);
     
    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;
    
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        
        DisplayCoordinates();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    
    
    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        
        label.text = coordinates.x  + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
