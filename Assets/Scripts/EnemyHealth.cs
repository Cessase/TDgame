using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    
    [Tooltip("Adds amount to maxHitpoints when enemy dies")]
    [SerializeField] private int difficulty = 1;
    
    int currentHitPoints = 0;

    private Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficulty;
            enemy.RewardGold();
        }
    }
    
}
