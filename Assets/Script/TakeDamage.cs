using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float Health = 50f;
    public GameObject Parent;

    public EnemyCount EnemyCount;

    public void TakeDamageFunction(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
            EnemyCount.EnemyDetruit += 1;
            EnemyCount.CheckingIfAllEnemyAreDestroyed();
            return;
        }
    }


    public void Die()
    {
        Destroy(Parent);
    }
}
