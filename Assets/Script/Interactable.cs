using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Add or remove a event interaction to this game object
    public bool useEvents;
    //Message qui apparait quand le joueur regarde l'objet avec un raycast
    public string Message;

    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();   
        }
        Interact();
    }

    protected virtual void Interact()
    {
        
    }
}
