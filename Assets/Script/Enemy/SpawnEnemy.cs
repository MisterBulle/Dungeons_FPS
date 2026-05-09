using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public List<GameObject> enemies;

    public List<Enemy_Path> Path;
    public Transform spawnPosition;

    private int ChoiceEnemy;

    void Start()
    {
        //Level 1
        //int NumberOfEnemy = 0;
        //Numéro entre 3 et 4
        int NumberOfEnemy = Random.Range(3,5);

        for (int i = 0; i < NumberOfEnemy; i++)
        {
            //Choix de l'ennemi à spawn
            ChoiceEnemy = Random.Range(0, enemies.Count);
            //Spawn de l'ennemi
            GameObject spawnedEnemy = Instantiate(enemies[ChoiceEnemy], spawnPoints[i].position, spawnPoints[i].rotation);
            //On lui donne l'item
            spawnedEnemy.GetComponent<Enemy>().enemy_path = Path[i];
        }
            

        //ChoiceEnemy = Random.Range(0, enemies.Count);
        
        //Debug.Log("Enemy Spawned : " + ChoiceEnemy);


        //Spawn();
    }

    void Spawn()
    {
        //Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);

        Instantiate(enemies[ChoiceEnemy], spawnPosition.position, spawnPosition.rotation);
    }
}
