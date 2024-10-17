using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager
    [SerializeField] private WeaponManager weaponManager;


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool sprintInput;
    public Vector2 movementInput;
    public float moveAmount;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    [Header("Interaction Inputs")]
    public bool fireInput = true;

    public bool isPauseKeyPressed = false;


    public void HandleAllInputs()
    {
        HandleFireInput();
        HandleMovementInput();
        HandleSprintingInput();
        HandleCameraInput();
        HandlePauseKeyInput();
    }

    void OnMove(InputValue value) { movementInput = value.Get<Vector2>(); }
    void OnLook(InputValue value) { cameraInput = value.Get<Vector2>(); }
    void OnSprint(InputValue value) { sprintInput = value.Get<float>() == 1; }
    void OnJump(InputValue value) { playerLocomotionHandler.HandleJump(); }
    void OnFire(InputValue value) { Debug.LogException(new NotImplementedException("This is implemented in 1.4.0")); }

    private void HandleCameraInput()
    {
        // Get mouse input for the camera
        //cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Get scroll input for camera zoom
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Send inputs to CameraManager
        cameraManager.zoomInput = scrollInput;
        cameraManager.cameraInput = cameraInput;
    }

    private void HandleMovementInput()
    {
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (sprintInput && moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        jumpInput = Input.GetKeyDown(KeyCode.Space); // Detect jump input (spacebar)
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }

    void HandleFireInput()
    {
        fireInput = Input.GetMouseButtonDown(0);
        if (fireInput) { weaponManager.Fire(); }
    }
}
