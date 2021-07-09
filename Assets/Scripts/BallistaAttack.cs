using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaAttack : MonoBehaviour
{
    
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectile;
    [SerializeField] private float towerRange = 1.5f;
    Transform target;
    Transform closestTarget;
    // Start is called before the first frame update
    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void AimWeapon()
    {
        float shootRange = Vector3.Distance(transform.position, target.position);
        
        weapon.LookAt(target);
        Attack(shootRange <= towerRange);
        
    }

    void Attack(bool isActive)
    {
        var projectileEmission = projectile.emission;
        projectileEmission.enabled = isActive;
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }
}
