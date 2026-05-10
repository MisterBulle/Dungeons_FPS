using UnityEngine;
using System.Collections.Generic;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] 
    //private GameObject door;
    private List<GameObject> door;
    [HideInInspector]
    public bool doorOpen;



    public void OpenDoorFunction(int doorNumber)
    {
        //C'est ici qu'on met le code pour modifier
        
        Debug.Log("Interaction avec" + gameObject.name);
        
        //base.Interact()

        doorOpen = !doorOpen;
        // On va chercher dans l'animator de l'objet le boolean "isOpen" et on lui donne la value true
            door[doorNumber].GetComponent<Animator>().SetBool("isOpen", doorOpen);

        
        //door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }

    public void CloseDoorFunction(int doorNumber)
    {
        doorOpen = false;
        door[doorNumber].GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
