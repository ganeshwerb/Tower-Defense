using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Settings")]
    public Transform firePoint;
    public int cost;
    public int damage;
    public GameObject projectilePrefab;

    private List<Enemy> enemiesInRange = new List<Enemy>();
    [SerializeField] private float fireCooldown = 0f;
    private void Start()
    {
        InvokeRepeating(nameof(TowerBehaviour),0,fireCooldown);
    }
    private void TowerBehaviour()
    {
        if (enemiesInRange.Count > 0)
        {
            RemoveDeadEnemies();

            if (enemiesInRange.Count > 0)
            {
                    FireAtEnemy();
            }
        }
    }

    private void FireAtEnemy()
    {
        if (enemiesInRange.Count > 0)
        {
            Enemy target = enemiesInRange[0];

            if (target != null && target.IsAlive())
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.Initialize(target.gameObject, damage);
            }
            else
            {
                enemiesInRange.RemoveAt(0); // Remove dead or invalid enemy
            }
        }
    }

    private void RemoveDeadEnemies()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null || !enemy.IsAlive() || !enemy.gameObject.activeInHierarchy);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemy.IsAlive() && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
