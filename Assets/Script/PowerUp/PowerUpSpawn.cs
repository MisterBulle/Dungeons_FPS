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
            Instantiate(powerUpPrefabs[randomnumber], SpawnPointList[i].position, Quaternion.identity, SpawnPointList[i]);
            powerUpPrefabs.RemoveAt(randomnumber);
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
