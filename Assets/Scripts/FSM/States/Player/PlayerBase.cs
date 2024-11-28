using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int hitPoints = 2000;
    public static UnityEvent OnGameOver = new();

    private void Start()
    {
        UIManager.Instance.baseHp.text = "HP: " + hitPoints.ToString();
    }
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        UIManager.Instance.baseHp.text = "HP: " + hitPoints.ToString();
        if (hitPoints <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
}
