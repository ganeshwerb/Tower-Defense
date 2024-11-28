using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private int damage;
    [SerializeField] private Rigidbody rb;

    public void Initialize(GameObject target, int damage)
    {
        this.damage = damage;
        Destroy(gameObject, 5f);
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.LookAt(target.transform,Vector3.up);
            rb.AddForce(speed * transform.forward,ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyScript = other.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
