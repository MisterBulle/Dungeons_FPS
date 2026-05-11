using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public List<Transform> spawnPointsLevel1;
    public List<Transform> spawnPointsLevel2;
    public List<GameObject> enemies;

    public List<Enemy_Path> Path;
    public List<Enemy_Path> Path2;
    //public Transform spawnPosition;

    public int NumberOfEnemy;

    private int ChoiceEnemy;


    [SerializeField]
    private int currentLevel = 1;
    
    private GameObject spawnedEnemy;

    public int MinEnemies;
    public int MaxEnemies;

    void Start()
    {
        MinEnemies = 5;
        MaxEnemies = 7;
    }

    public void Spawn()
    {
        NumberOfEnemy = Random.Range(MinEnemies, MaxEnemies);
        Debug.Log("Number of enemy " + NumberOfEnemy);

        EnemyCount enemyCountRef = FindObjectOfType<EnemyCount>();
        if (enemyCountRef != null)
        {
            enemyCountRef.EnemyTotalSpawn = NumberOfEnemy;
        }

        List<Transform> currentSpawnPoints = currentLevel == 1 ? spawnPointsLevel1 : spawnPointsLevel2;
        List<Enemy_Path> currentPaths = currentLevel == 1 ? Path : Path2;
        int availableSpawnCount = currentSpawnPoints != null ? currentSpawnPoints.Count : 0;
        int availablePathCount = currentPaths != null ? currentPaths.Count : 0;
        int spawnCount = Mathf.Min(NumberOfEnemy, availableSpawnCount, availablePathCount);

        if (spawnCount <= 0)
        {
            Debug.LogWarning("SpawnEnemy.Spawn: no spawn points or enemy paths available.");
            return;
        }

        if (spawnCount < NumberOfEnemy)
        {
            Debug.LogWarning($"SpawnEnemy.Spawn requested {NumberOfEnemy} enemies but only {spawnCount} spawn points/paths are available.");
            NumberOfEnemy = spawnCount;
        }

        for (int i = 0; i < NumberOfEnemy; i++)
        {
            if (enemies == null || enemies.Count == 0)
            {
                Debug.LogWarning("SpawnEnemy.Spawn: no enemies configured in the enemies list.");
                break;
            }

            ChoiceEnemy = Random.Range(0, enemies.Count);
            if (currentSpawnPoints == null || currentSpawnPoints.Count <= i)
            {
                Debug.LogWarning($"SpawnEnemy.Spawn: missing spawn point for index {i}.");
                break;
            }

            spawnedEnemy = Instantiate(enemies[ChoiceEnemy], currentSpawnPoints[i].position, currentSpawnPoints[i].rotation);
            if (currentPaths != null && currentPaths.Count > i)
            {
                Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.enemy_path = currentPaths[i];
                }
            }

            if (enemyCountRef != null)
            {
                TakeDamage takeDamage = spawnedEnemy.GetComponent<TakeDamage>();
                if (takeDamage != null)
                {
                    takeDamage.enemyCount = enemyCountRef;
                }
            }
        }

        currentLevel++;
        MinEnemies = 10;
        MaxEnemies = 12;
    }

    /*void Spawn()
    {
        //Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation);

        Instantiate(enemies[ChoiceEnemy], spawnPosition.position, spawnPosition.rotation);
    }*/
}
