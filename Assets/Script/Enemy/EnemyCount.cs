using UnityEngine;

public class EnemyCount : MonoBehaviour
{

    public int EnemyDetruit = 0;
    public int EnemyTotalSpawn = 3;

    public Interactable_Test I_T;


    
    void Start()
    {
        
    }

    public void CheckingIfAllEnemyAreDestroyed()
    {
        if (EnemyDetruit == EnemyTotalSpawn)
        {
            Debug.Log("Tous les ennemis sont détruits !");
            EnemyDetruit = 0;
            //On a détruit tous les ennemis on peut de nouveau ouvrir la porte
            I_T.CanIOpenTheDoor = true;
            //Je lance le spawn des powerup
            GetComponent<PowerUpSpawn>().LaunchPowerUpSpawn();

        }
        else
        {
            Debug.Log("Il reste encore des ennemis à détruire !");
        }
    }


}
