using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;


    public Vector2 movementInput;
    public Vector2 cameraMovementInput;
    public float verticalInput;
    public float horizontalInput;
    private float movementAmount;
    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null) { 
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.CameraMovement.performed += i => cameraMovementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraMovementInput.x;
        cameraInputY = cameraMovementInput.y;

        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.ChangeAnimatorValues(0, movementAmount);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}
