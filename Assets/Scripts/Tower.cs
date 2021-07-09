using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool createTower(Tower tower, Vector3 position)
    {
        Currency bank = FindObjectOfType<Currency>();
        if(bank == null){ return false; }

        if (bank.CurrentBalance >= towerCost)
        {
            Instantiate(tower,position,Quaternion.identity);
            bank.Withdraw(towerCost);
            return true;
        }

        return false;

    }
}
