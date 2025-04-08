using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameragameObject;

    Rigidbody playerRigidbody;

    public float movementSpeed = 2f;
    public float rotationSpeed = 13f;



    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameragameObject = Camera.main.transform;
    }

    void HandleMovement()
    {
        moveDirection = new Vector3(cameragameObject.forward.x, 0f, cameragameObject.forward.z) * inputManager.verticalInput;
        moveDirection = moveDirection + cameragameObject.right * inputManager.horizontalInput;

        moveDirection.Normalize();

        moveDirection.y = 0;

        moveDirection = moveDirection* movementSpeed;

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
        HandleMovement();
        HandleRotation();
    }
}
