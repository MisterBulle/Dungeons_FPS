using UnityEngine;
using System.Collections;

public class IsPlayerExitDoor : MonoBehaviour
{

    public LaunchVague launchVague;

    public bool isExitDoor = false;

    void Start()
    {
        isExitDoor = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isExitDoor = true;
            //On peut lancer la première vague
            launchVague.LaunchVagueFunction();
            Debug.Log("EXIT");
            Destroy(gameObject);
        }
    }

}
