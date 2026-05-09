using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] 
    private GameObject door;
    private bool doorOpen;

    void Start()
    {
        //doorOpen = true;
        //door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }


    public void OpenDoorFunction()
    {
        //C'est ici qu'on met le code pour modifier
        
        Debug.Log("Interaction avec" + gameObject.name);
        
        //base.Interact()

        doorOpen = !doorOpen;
        // On va chercher dans l'animator de l'objet le boolean "isOpen" et on lui donne la value true
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
