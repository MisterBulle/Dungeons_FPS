using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControlls_PI playerControll;
    //private PlayerControlls_PI onFoot;
    PlayerControlls_PI.OnFootActions onFoot;

    //Call function
    private PlayerMotor PM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerControll = new PlayerControlls_PI();
        //Récupération du onfoot du playerInput de l'input manager
        onFoot = playerControll.OnFoot;

        PM = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PM.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
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
