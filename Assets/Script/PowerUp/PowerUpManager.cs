using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }
    public List<PowerUp> activePowerUps = new List<PowerUp>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            PowerUp powerUp = activePowerUps[i];
            if (powerUp == null)
            {
                activePowerUps.RemoveAt(i);
                continue;
            }

            powerUp.Tick();

            if (powerUp.ShouldRemove)
            {
                RemoveActivePowerUp(powerUp);
            }
        }
    }

    public void AddActivePowerUp(PowerUp powerUpPrefab, GameObject player)
    {
        if (powerUpPrefab == null || player == null)
        {
            Debug.LogWarning("PowerUpManager.AddActivePowerUp: powerUpPrefab ou player est null.");
            return;
        }

        GameObject activeObject = new GameObject($"{powerUpPrefab.title}_Active");
        activeObject.transform.SetParent(transform);

        PowerUp activePowerUp = powerUpPrefab.Clone(activeObject);
        activePowerUp.Initialize(player);
        activePowerUp.Apply(player);

        activePowerUps.Add(activePowerUp);
    }

    public void RemoveActivePowerUp(PowerUp activePowerUp)
    {
        if (activePowerUp == null)
            return;

        if (activePowerUps.Contains(activePowerUp))
            activePowerUps.Remove(activePowerUp);

        Destroy(activePowerUp.gameObject);
    }
}
