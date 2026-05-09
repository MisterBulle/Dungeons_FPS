using UnityEngine;

public class EnemyCount : MonoBehaviour
{

    public int EnemyDetruit = 0;
    public int EnemyTotalSpawn = 3;

    private OpenDoor openDoor;

    void Start()
    {
        openDoor = GetComponent<OpenDoor>();
    }

    public void CheckingIfAllEnemyAreDestroyed()
    {
        if (EnemyDetruit >= EnemyTotalSpawn)
        {
            Debug.Log("Tous les ennemis sont détruits !");
            openDoor.OpenDoorFunction();
        }
        else
        {
            Debug.Log("Il reste encore des ennemis à détruire !");
        }
    }


}
