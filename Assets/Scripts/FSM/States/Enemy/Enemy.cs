using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 20; 
    [SerializeField] private NavMeshAgent agent;

    private PlayerBase playerBase;
    private EnemyState spawner;
    private int currentHealth;
    private bool isPooled = false;

    public void Initialize(PlayerBase baseTarget, EnemyState spawnerRef)
    {
        playerBase = baseTarget;
        spawner = spawnerRef;
        currentHealth = maxHealth;
        isPooled = false; 
        gameObject.SetActive(true);
        agent.enabled = true;
        agent.destination = playerBase.transform.position;
    }

    public bool IsAlive() => currentHealth > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (isPooled) return;

        PlayerBase baseTarget = other.GetComponent<PlayerBase>();
        if (baseTarget != null)
        {
            baseTarget.TakeDamage(damage);
            PoolEnemy(); 
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isPooled) return; 

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            PoolEnemy(); 
            GoldManager.KilledEnemy();
            UIManager.Instance.UpdateGold();
        }
    }

    private void PoolEnemy()
    {
        if (isPooled) return; // Prevent multiple pooling calls
        isPooled = true;

        agent.ResetPath(); // Stop movement
        agent.enabled = false;
        gameObject.SetActive(false); // Deactivate enemy
        spawner.ReturnEnemyToPool(gameObject); // Return to pool
    }
}
