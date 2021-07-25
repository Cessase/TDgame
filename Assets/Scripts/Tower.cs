using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost;
    [SerializeField] private float buildDelay = 1f;
    
    void Start()
    {
        StartCoroutine(BuildTower());
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

    IEnumerator BuildTower()
    {
        foreach (Transform child in transform)
        {
            gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                gameObject.SetActive(true);
            }
        }
        
    }
}
