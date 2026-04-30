using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float healAmount = 20f;

    private float a = 0;

    private void OnTriggerEnter(Collider other)
    {
        a = playerHealth.playerhealth;
        if (other.CompareTag("Player") && playerHealth.playerhealth != playerHealth.maxHealth)
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }

        //Il faut pas qu'il dépasse le montant max de santé
        if(a > playerHealth.maxHealth)
        {
            playerHealth.playerhealth = playerHealth.maxHealth;
        }
    }
}
