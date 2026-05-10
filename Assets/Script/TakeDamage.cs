using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float Health = 50f;
    public GameObject Parent;
    public EnemyCount enemyCount;

    void Start()
    {
        if (enemyCount == null)
        {
            enemyCount = FindObjectOfType<EnemyCount>();
        }
    }

    public void TakeDamageFunction(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
            enemyCount.EnemyDetruit += 1;
            enemyCount.CheckingIfAllEnemyAreDestroyed();
            return;
        }
    }


    public void Die()
    {
        Destroy(Parent);
    }
}
