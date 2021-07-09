using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    private Currency bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Currency>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        
        bank.Deposit(goldReward);
    }
    
    public void StealGold()
    {
        if (bank == null) { return; }
        
        bank.Withdraw(goldPenalty);
    }
}
