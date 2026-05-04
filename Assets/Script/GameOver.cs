using UnityEngine;

public class GameOver : MonoBehaviour
{

    public PlayerHealth playerHealth;

    void Update()
    {
        if (playerHealth.playerhealth <= 0)
        {
            //Vu qu'il sont dans le même gameObject
            GetComponent<SceneManagerFunction>().LoadScene("GameOver");
            Debug.Log("Game Over");
        }
    }
}
