using UnityEngine;

public class Interactable_Test : Interactable
{

    [SerializeField] 
    private GameObject door;
    private bool doorOpen;


    protected override void Interact()
    {
        //C'est ici qu'on met le code pour modifier
        
        Debug.Log("Interaction avec" + gameObject.name);
        
        //base.Interact()

        doorOpen = !doorOpen;
        // On va chercher dans l'animator de l'objet le boolean "isOpen" et on lui donne la value true
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
