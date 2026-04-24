using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Vu que PlayerInteract et PlayerLook sont sur le même GameObject on peut récupérer la caméra depuis PlayerLook
    private Camera cam;

    [SerializeField] 
    private float interactDistance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        playerUI.UpdateText(string.Empty);

        //Création d'un raycast au centre de la caméra
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //Afficage du raycast dans la scène pour le debug
        Debug.DrawRay(ray.origin, ray.direction * interactDistance);
        //Variable pour stocker les informations du raycast
        RaycastHit hitInfo; 
        //Si le raycast touche quelque chose avec le tag mask
        if (Physics.Raycast(ray, out hitInfo, interactDistance,mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //Affichage du message de l'objet interactable
                //Debug.Log(hitInfo.collider.GetComponent<Interactable>().Message);  
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.Message);

                //Activation de l'input manager avec Interact
                //Activation de la touche "E"
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();   
                }
            }
        }


    }

}
