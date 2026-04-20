using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControlls_PI playerControll;
    //private PlayerControlls_PI onFoot;
    PlayerControlls_PI.OnFootActions onFoot;

    //Call function
    private PlayerMotor PM;

    private PlayerLook PL;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerControll = new PlayerControlls_PI();
        //Récupération du onfoot du playerInput de l'input manager
        onFoot = playerControll.OnFoot;
        PM = GetComponent<PlayerMotor>();
        PL = GetComponent<PlayerLook>();
        //Event de jump, quand on appuie sur le bouton de jump, on appelle la fonction jump du player motor
        //Event : 3 états : started, performed, canceled
        onFoot.Jump.performed += ctx => PM.Jump();    
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        PM.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        PL.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
