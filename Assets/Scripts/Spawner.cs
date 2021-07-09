using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(0,50)] int poolSize= 5;
    [SerializeField] [Range(0.1f,5f)] private float spawnTimer;
    // Start is called before the first frame update

    private GameObject[] enemyPool;

    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulatePool()
    {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = Instantiate(enemy, transform);
            enemyPool[i].SetActive(false);
        }
    }

    IEnumerator EnemySpawner()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeInHierarchy == false)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }
}
