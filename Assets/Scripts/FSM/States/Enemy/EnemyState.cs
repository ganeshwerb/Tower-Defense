using System.Collections;
using UnityEngine;

public class EnemyState : MonoBehaviour,IState
{
    [SerializeField] private PlayerBase playerBase;
    [SerializeField] private Transform[] spawnTransforms;
    [SerializeField] private ObjectPool enemyPool;
    public int enemyCount = 25;
    public GameManager gameManager;

    public void Init()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    public void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                ReturnEnemyToPool(transform.GetChild(i).gameObject);
        }
    }

    public void Tick()
    {
    }
    private IEnumerator SpawnEnemyCoroutine()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(.1f);
        }
    }
    public void SpawnEnemy()
    {
        Transform spawnPoint = spawnTransforms[Random.Range(0, spawnTransforms.Length)];
        GameObject enemyObject = enemyPool.Get();
        if (enemyObject != null)
        {
            enemyObject.transform.SetPositionAndRotation(spawnPoint.position + Vector3.up * 0.5f, spawnPoint.rotation);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Initialize(playerBase, this);
            }
            else
            {
            }
        }
        else
        {
        }
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        if (enemyPool != null)
        {
            enemyPool.Return(enemy);
        }
        else
        {
        }
    }
}
