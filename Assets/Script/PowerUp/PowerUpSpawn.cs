using UnityEngine;
using System.Collections.Generic;

public class PowerUpSpawn : MonoBehaviour
{
    public List<GameObject> powerUpPrefabs;

    [SerializeField]
    private List<GameObject> powerUpSave;
    public List<GameObject> spawnPoint;

    [SerializeField]
    private List<Transform> SpawnPointList;
    public int NumberOfSpawnPowerUp;

    void Start()
    {
        NumberOfSpawnPowerUp = 0;
        
        // Initialiser powerUpSave une SEULE fois avec tous les power-ups
        powerUpSave = new List<GameObject>(powerUpPrefabs);
        
        Debug.Log($"PowerUpSpawn.Start: {powerUpSave.Count} power-ups disponibles au total.");

        LaunchPowerUpSpawn();
    }

    public void LaunchPowerUpSpawn()
    {
        // Réinitialiser SpawnPointList à chaque appel
        SpawnPointList = new List<Transform>();
        
        // Créer une liste TEMPORAIRE pour ce spawn (ne pas toucher à powerUpSave)
        List<GameObject> availablePowerUps = new List<GameObject>(powerUpSave);
        
        // Récupérer les points de spawn
        Transform spawnContainer = spawnPoint[NumberOfSpawnPowerUp].transform;
        int childCount = spawnContainer.childCount;
        for (int i = 0; i < childCount; i++)
        {
            SpawnPointList.Add(spawnContainer.GetChild(i));
        }
        
        ChoicePowerUp(availablePowerUps);
    }

    void ChoicePowerUp(List<GameObject> availablePowerUps)
    {
        Debug.Log("ChoicePowerUp() appelé");
        Debug.Log("SpawnPointList.Count = " + SpawnPointList.Count);
        Debug.Log("availablePowerUps.Count = " + availablePowerUps.Count);
        
        for (int i = 0; i < SpawnPointList.Count; i++)
        {
            Debug.Log("Boucle itération " + i);
            
            // Vérifier qu'il y a encore des PowerUp disponibles pour ce spawn
            if (availablePowerUps.Count == 0)
            {
                Debug.LogWarning("Plus de PowerUp disponibles pour ce spawn !");
                break;
            }
            
            // Choisir aléatoirement un power-up
            int randomnumber = Random.Range(0, availablePowerUps.Count);
            GameObject spawnedPowerUp = Instantiate(availablePowerUps[randomnumber], SpawnPointList[i].position, Quaternion.identity, SpawnPointList[i]);
            Debug.Log("PowerUp spawnné : " + spawnedPowerUp.name);
            
            // Assigner les références manquantes avec Reflection
            PowerUp powerUpComponent = spawnedPowerUp.GetComponentInChildren<PowerUp>();
            if (powerUpComponent != null)
            {
                var fields = powerUpComponent.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(GameObject))
                    {
                        Transform child = FindDeepChild(spawnedPowerUp.transform, field.Name);
                        if (child != null)
                        {
                            field.SetValue(powerUpComponent, child.gameObject);
                        }
                    }
                }
            }
            
            // Assigner Player et PowerUpSpawn pour Interact_PowerUp
            Interact_PowerUp interactPowerUp = spawnedPowerUp.GetComponentInChildren<Interact_PowerUp>();
            if (interactPowerUp != null)
            {
                GameObject playerFound = GameObject.FindGameObjectWithTag("Player");
                interactPowerUp.player = playerFound;
                interactPowerUp.PowerUpSpawn = this;
            }
            
            // Retirer du choix TEMPORAIRE seulement (pour éviter les doublons dans ce spawn)
            availablePowerUps.RemoveAt(randomnumber);
            
            // Initialiser le PowerUp avec le player
            PowerUp powerUp = powerUpComponent;
            if (powerUp != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    powerUp.Initialize(player);
                }
            }
        }
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    public void RemoveChoicePowerUp(string powerUpTitle)
    {
        Debug.Log($"RemoveChoicePowerUp appelé avec title: '{powerUpTitle}'");
        Debug.Log($"powerUpSave.Count au moment de la recherche: {powerUpSave.Count}");
        
        // Rechercher et VRAIMENT supprimer le PowerUp de la liste persistante powerUpSave
        for (int i = 0; i < powerUpSave.Count; i++)
        {
            PowerUp powerUpComponent = powerUpSave[i].GetComponent<PowerUp>();
            if (powerUpComponent != null)
            {
                Debug.Log($"Vérification [{i}]: prefab='{powerUpSave[i].name}', component.title='{powerUpComponent.title}'");
                
                // Chercher par title OU par nom du prefab (fallback)
                bool titleMatch = powerUpComponent.title == powerUpTitle;
                bool nameMatch = powerUpSave[i].name.Contains(powerUpTitle);
                
                if (titleMatch || nameMatch)
                {
                    Debug.Log($"✓ Match trouvé ({(titleMatch ? "title" : "name")}). Suppression...");
                    powerUpSave.RemoveAt(i);
                    Debug.Log($"✓ PowerUp '{powerUpTitle}' supprimé de powerUpSave. Restants: {powerUpSave.Count}");
                    return;
                }
            }
            else
            {
                Debug.LogWarning($"powerUpSave[{i}] n'a pas de composant PowerUp!");
            }
        }
        
        Debug.LogWarning($"✗ PowerUp '{powerUpTitle}' NOT trouvé dans powerUpSave!");
    }
}
