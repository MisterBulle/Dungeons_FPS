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

        LaunchPowerUpSpawn();
    }

    public void LaunchPowerUpSpawn()
    {
        // Réinitialiser SpawnPointList à chaque appel
        SpawnPointList = new List<Transform>();
        
        //Save des powerup
        powerUpSave = new List<GameObject>(powerUpPrefabs);
        
        //On récupère les 3 enfants de spawnPoint
        for (int i = 0; i <= 2; i++)
        {
            SpawnPointList.Add(spawnPoint[NumberOfSpawnPowerUp].transform.GetChild(i));
        }
        ChoicePowerUp();
    }


    void ChoicePowerUp()
    {

        Debug.Log("ChoicePowerUp() appelé");
        Debug.Log("SpawnPointList.Count = " + SpawnPointList.Count);
        Debug.Log("powerUpSave.Count = " + powerUpSave.Count);
        
        for (int i = 0; i < SpawnPointList.Count; i++)
        {
            Debug.Log("Boucle itération " + i);
            
            // Vérifier qu'il y a encore des PowerUp disponibles
            if (powerUpSave.Count == 0)
            {
                Debug.LogWarning("Plus de PowerUp disponibles !");
                break;
            }
            
            //4 est exclus
            int randomnumber = Random.Range(0, powerUpSave.Count);
            GameObject spawnedPowerUp = Instantiate(powerUpSave[randomnumber], SpawnPointList[i].position, Quaternion.identity, SpawnPointList[i]);
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
            
            powerUpSave.RemoveAt(randomnumber);
            
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

    public void RemoveChoicePowerUp(string powerUpName)
    {
        // Rechercher et supprimer le PowerUp de la liste
        for (int i = 0; i < powerUpSave.Count; i++)
        {
            if (powerUpSave[i].name == powerUpName)
            {
                powerUpSave.RemoveAt(i);
                break;
            }
        }
    }

}
