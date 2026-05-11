using UnityEngine;
using System.Collections;


public class PlayerMotor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed;

    public float originalSpeed;
    public float MaxSpeed;

    public bool isGrounded;
    public float gravity = -9.81f;
    
    public float JumpHeight;

    //Dashing
    public bool canDash = true;
    public bool isDashing = false;

    [Header("Dash settings")]
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Load stats from DataManager
        if (DataManager.Instance != null)
        {
            speed = DataManager.Instance.playerStats.speed;
            MaxSpeed = DataManager.Instance.playerStats.maxSpeed;
            JumpHeight = DataManager.Instance.playerStats.jumpHeight;
            dashingPower = DataManager.Instance.playerStats.dashingPower;
            dashingTime = DataManager.Instance.playerStats.dashingTime;
            dashingCooldown = DataManager.Instance.playerStats.dashingCooldown;
        }

        originalSpeed = speed;
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
            speed = originalSpeed;
        }
    }

    //Fonction pour lancer Dash
    public void StartDash(Vector2 input)
    {
        if (!isDashing && canDash)
        {
            StartCoroutine(Dash(input));
        }
    }

    public IEnumerator Dash(Vector2 input)
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection;

        if (input.magnitude > 0.1f)
        {
            dashDirection = transform.TransformDirection(new Vector3(input.x, 0f, input.y)).normalized;
        }
        else
        {
            dashDirection = transform.forward;
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashingTime)
        {
            controller.Move(dashDirection * dashingPower * Time.deltaTime);
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
