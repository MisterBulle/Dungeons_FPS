using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //reçois les inputs de l'input manager et on l'apply au character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    }
}
