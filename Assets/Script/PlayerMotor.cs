using UnityEngine;
using System.Collections;


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

    //Dashing
    private bool canDash = false;
    private bool isDashing = false;

    [Header("Dash settings")]
    public float dashingPower = 10f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

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

    //Fonction pour lancer Dash
    public void StartDash()
    {
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        //float originalGravity = gravity;
        //gravity = 0f;
        //playerVelocity.y = 0f;


        float startTime = Time.time;
        while (Time.time < startTime + dashingTime)
        {
            Vector3 dashDirection = transform.forward;
            controller.Move(dashDirection * dashingPower * Time.deltaTime);
            yield return null; // attend la frame suivante
        }   

        //gravity = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
