using UnityEngine;

public class LaunchVague : MonoBehaviour
{

    public IsPlayerExitDoor isPlayerExitDoor;

    private bool GetBoolFromIsPlayerExitDoor;

    public int NumberOfVague = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetBoolFromIsPlayerExitDoor = isPlayerExitDoor.isExitDoor;
    }

    public void LaunchVagueFunction()
    {
        //On fait spawn les ennemis
        GetComponent<SpawnEnemy>().Spawn();
        //On ferme la porte
        //GetComponent<OpenDoor>().doorOpen = false;
        GetComponent<OpenDoor>().CloseDoorFunction(NumberOfVague);
        NumberOfVague++;

    }

    // Update is called once per frame
}
