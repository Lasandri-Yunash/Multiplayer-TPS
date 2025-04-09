using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    AnimatorManager animatorManager;
    Vector3 moveDirection;
    Transform cameragameObject;

    playerManager playerManager;

    Rigidbody playerRigidbody;


    [Header("Faling and Landing")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;


    [Header("Movement flags")]
    public bool isMoving;
    public bool isSprinting;
    public bool isGrounded;



    [Header("Movement value")]

    public float movementSpeed = 2f;
    public float rotationSpeed = 13f;

    public float sprintingSpeed = 7f;


    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameragameObject = Camera.main.transform;
        playerManager = GetComponent<playerManager>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    void HandleMovement()
    {
        moveDirection = new Vector3(cameragameObject.forward.x, 0f, cameragameObject.forward.z) * inputManager.verticalInput;
        moveDirection = moveDirection + cameragameObject.right * inputManager.horizontalInput;

        moveDirection.Normalize();

        moveDirection.y = 0;

        if(isSprinting )
        {
            moveDirection = moveDirection * sprintingSpeed;

        }
        else
        {
            if (inputManager.movementAmount >= 0.5f)
            {
                moveDirection = moveDirection * movementSpeed;
                isMoving = true;
            }

            if (inputManager.movementAmount >= 0f)
            {
                isMoving = false;
            }

        }


        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;

    }
    void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        
        targetDirection = cameragameObject.forward*inputManager.verticalInput;
        targetDirection = targetDirection + cameragameObject.right*inputManager.horizontalInput;    

        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) { 
            targetDirection = transform.forward;
        }

        Quaternion targetRotation  = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleAllMovements()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
    }

    void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        raycastOrigin.y = raycastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded)
        {
            Debug.Log("3 ");

            if (!playerManager.isInteracting)
            {
                Debug.Log("4 ");

                animatorManager.PlayerTargetAim("Falling", true);
            }


            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.position * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out hit, groundLayer))
        {
            Debug.Log("1 ");
            if (!isGrounded && !playerManager.isInteracting)
            {
                Debug.Log("2 ");

                animatorManager.PlayerTargetAim("Landing", true);

            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }
    }


}
