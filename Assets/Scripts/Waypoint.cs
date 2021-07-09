using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower tower;
    
    [SerializeField] private bool isPlaceable;

    // Start is called before the first frame update
    void Start()
    {
        
    } 

    public bool IsPlaceable
    {
        get => isPlaceable;
        set => isPlaceable = value;
    }
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = tower.createTower(tower, transform.position);
            isPlaceable = isPlaced;
        }
    }

    
}
