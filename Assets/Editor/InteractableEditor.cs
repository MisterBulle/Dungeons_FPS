using UnityEditor;

//Fonction permettant de customer l'inspecteur des objets
//En affichant ou non les events d'interaction (comme les boutons)

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    //Function appelé à chaque update
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.Message = EditorGUILayout.TextField("Message", interactable.Message);
            EditorGUILayout.HelpBox("Event Only Interactact can ONLY use UnityEvents", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {  
            //interactable.useEvents = true;
            base.OnInspectorGUI();
            if (interactable.useEvents)
            {
            //We are using event, add component
                if (interactable.gameObject.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                if (interactable.gameObject.GetComponent<InteractionEvent>() != null)
                {
                    //We are not using event, remove component
                    DestroyImmediate(interactable.gameObject.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
