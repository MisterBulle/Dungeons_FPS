using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public List<Transform> spawnPointsLevel1;
    public List<Transform> spawnPointsLevel2;
    public List<GameObject> enemies;

    public List<Enemy_Path> Path;
    //public Transform spawnPosition;

    public int NumberOfEnemy;

    private int ChoiceEnemy;


    [SerializeField]
    private int currentLevel = 1;
    
    private GameObject spawnedEnemy;

    void Start()
    {
    }

    public void Spawn()
    {
        //Level 1

        int MinEnemies = 5;
        int MaxEnemies = 7;
        
        NumberOfEnemy = Random.Range(MinEnemies, MaxEnemies);
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
            if (currentLevel == 1)
            {
                spawnedEnemy = Instantiate(enemies[ChoiceEnemy], spawnPointsLevel1[i].position, spawnPointsLevel1[i].rotation);
            }
            else
            {
                spawnedEnemy = Instantiate(enemies[ChoiceEnemy], spawnPointsLevel2[i].position, spawnPointsLevel2[i].rotation);
            }
            //GameObject spawnedEnemy = Instantiate(enemies[ChoiceEnemy], spawnPoints[i].position, spawnPoints[i].rotation);
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
        //Pour le level 2
        currentLevel++;
        MinEnemies = 10;
        MaxEnemies = 13;

    }

    /*void Spawn()
    {
        //Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);

        Instantiate(enemies[ChoiceEnemy], spawnPosition.position, spawnPosition.rotation);
    }*/
}
