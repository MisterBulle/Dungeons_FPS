using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public PlayerStatsData playerStats;
    public WeaponStatsData weaponStats;
    public EnemyStatsData enemyStats;
    public TankPowerUpStatsData tankPowerUpStats;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadData()
    {
        playerStats = LoadJson<PlayerStatsData>("Data/PlayerStats");
        weaponStats = LoadJson<WeaponStatsData>("Data/WeaponStats");
        enemyStats = LoadJson<EnemyStatsData>("Data/EnemyStats");
        tankPowerUpStats = LoadJson<TankPowerUpStatsData>("Data/TankPowerUpStats");
    }

    T LoadJson<T>(string path)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(path);
        if (jsonFile != null)
        {
            return JsonUtility.FromJson<T>(jsonFile.text);
        }
        else
        {
            Debug.LogError("JSON file not found: " + path);
            return default(T);
        }
    }
}