using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float MaxSpeed = 10f;

    public bool isGrounded;
    public float gravity = -9.81f;
    
    public float JumpHeight = 3f;

    //public bool sprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    //reçois les inputs de l'input manager et on l'apply au character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);

    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
        }
    }
    public void Sprint(bool isHolding)
    {
        if (isHolding)
        {
            speed = MaxSpeed;
        }
        else
        {
            speed = 5f;
        }
    }
}
