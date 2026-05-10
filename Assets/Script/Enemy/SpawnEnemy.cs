using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> enemies;

    public List<Enemy_Path> Path;
    public Transform spawnPosition;

    public int NumberOfEnemy;

    private int ChoiceEnemy;

    void Start()
    {
    }

    public void Spawn()
    {
        //Level 1
        //int NumberOfEnemy = 0;
        //Numéro entre 3 et 4
        
        NumberOfEnemy = Random.Range(3,5);
        //On trouve EnemyCount
        EnemyCount enemyCountRef = FindObjectOfType<EnemyCount>();
        
        if (enemyCountRef != null)
        {
            enemyCountRef.EnemyTotalSpawn = NumberOfEnemy;
        }
        
        for (int i = 0; i < NumberOfEnemy; i++)
        {
            //Choix de l'ennemi à spawn
            ChoiceEnemy = Random.Range(0, enemies.Count);
            //Spawn de l'ennemi
            GameObject spawnedEnemy = Instantiate(enemies[ChoiceEnemy], spawnPoints[i].position, spawnPoints[i].rotation);
            //On lui donne l'item
            spawnedEnemy.GetComponent<Enemy>().enemy_path = Path[i];
            //On lui assigne la référence EnemyCount
            if (enemyCountRef != null)
            {
                TakeDamage takeDamage = spawnedEnemy.GetComponent<TakeDamage>();
                if (takeDamage != null)
                {
                    takeDamage.enemyCount = enemyCountRef;
                }
            }
        }
    }

    /*void Spawn()
    {
        //Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);

        Instantiate(enemies[ChoiceEnemy], spawnPosition.position, spawnPosition.rotation);
    }*/
}
