using UnityEngine;
using System.Collections.Generic;

public class PowerUpSpawn : MonoBehaviour
{
    public List<GameObject> powerUpPrefabs;

    [SerializeField]
    private List<GameObject> powerUpSave;
    public GameObject spawnPoint;

    [SerializeField]
    private List<Transform> SpawnPointList;

    void Start()
    {
        LaunchPowerUpSpawn();
    }

    public void LaunchPowerUpSpawn()
    {
        //Save des powerup
        powerUpSave = new List<GameObject>(powerUpPrefabs);
        //On récupère les 3 enfants de spawnPoint
        for (int i = 0; i <= 2; i++)
        {
            SpawnPointList.Add(spawnPoint.transform.GetChild(i));
        }
        ChoicePowerUp();
    }


    void ChoicePowerUp()
    {
        for (int i = 0; i < SpawnPointList.Count; i++)
        {
            //4 est exclus
            int randomnumber = Random.Range(0, powerUpPrefabs.Count);
            GameObject spawnedPowerUp = Instantiate(powerUpPrefabs[randomnumber], SpawnPointList[i].position, Quaternion.identity, SpawnPointList[i]);
            
             

            // Assigner les références manquantes avec Reflection
            /*PowerUp powerUpComponent = spawnedPowerUp.GetComponent<PowerUp>();
            if (powerUpComponent != null)
            {
                var fields = powerUpComponent.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(GameObject))
                    {
                        Transform child = spawnedPowerUp.transform.Find(field.Name);
                        if (child != null)
                        {
                            field.SetValue(powerUpComponent, child.gameObject);
                        }
                    }
                }
            }
            
            // Assigner Player et PowerUpSpawn pour Interact_PowerUp
            Interact_PowerUp interactPowerUp = spawnedPowerUp.GetComponent<Interact_PowerUp>();
            if (interactPowerUp != null)
            {
                interactPowerUp.player = GameObject.FindGameObjectWithTag("Player");
                interactPowerUp.PowerUpSpawn = this;
            }
            
            powerUpPrefabs.RemoveAt(randomnumber);*/
        }
    }

    public void RemoveChoicePowerUp(string powerUpName)
    {
        GameObject removedPrefab = null;

        for (int i = 0; i < powerUpSave.Count; i++)
        {
            if (powerUpSave[i].name == powerUpName)
            {
                removedPrefab = powerUpSave[i];
                powerUpSave.RemoveAt(i);
                break;
            }
        }

        if (removedPrefab != null)
        {
            powerUpPrefabs.Remove(removedPrefab);
        }
        else
        {
            powerUpPrefabs.RemoveAll(prefab => prefab != null && prefab.name == powerUpName);
        }

        powerUpPrefabs = new List<GameObject>(powerUpSave);
        powerUpSave = powerUpPrefabs;
    }

}
