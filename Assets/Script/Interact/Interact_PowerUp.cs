using UnityEngine;

public class Interact_PowerUp : Interactable
{
    public PowerUp powerUp;
    public GameObject player; 

    public PowerUpSpawn PowerUpSpawn;

    protected override void Interact()
    {
        if (powerUp == null || player == null)
            return;

        if (PowerUpManager.Instance != null)
        {
            PowerUpManager.Instance.AddActivePowerUp(powerUp, player);
        }
        else
        {
            powerUp.Apply(player);
        }

        // Destruction des 2 autres objets
        Destroy(PowerUpSpawn.spawnPoint[PowerUpSpawn.NumberOfSpawnPowerUp].gameObject);
        PowerUpSpawn.RemoveChoicePowerUp(powerUp.title);

        powerUp.DestroyPowerUp();
        PowerUpSpawn.NumberOfSpawnPowerUp++;
        //PowerUpSpawn.LaunchPowerUpSpawn();
    }
}
