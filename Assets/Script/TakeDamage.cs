using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float Health = 50f;

    public void TakeDamageFunction(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
            return;
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }
}
