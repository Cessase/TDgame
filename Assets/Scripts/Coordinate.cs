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
     
    private TextMeshPro label;
    private Vector2Int coordinates;
    private Waypoint waypoint;
    
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        
        waypoint = GetComponentInParent<Waypoint>();
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
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z);
        
        label.text = coordinates.x  + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
