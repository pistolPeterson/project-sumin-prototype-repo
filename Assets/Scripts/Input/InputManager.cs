using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public InputAction movement;
    public InputAction shield;
    [Header("Player Inputs")] public PlayerControls playerControls;
    private Vector2 currMoveDir;

    [HideInInspector] public UnityEvent OnShieldPressed;
    [HideInInspector] public UnityEvent OnShieldReleased;
    [HideInInspector] public UnityEvent<Vector2> OnMovement;
    
  
    private void Awake() {
        playerControls = new PlayerControls();
    }
    private void Update() {
        shield.started += HoldingShield;
        shield.canceled += ReleasingShield;
        shield.performed += UseShield;
        movement.performed += Move;
    }
    private void OnEnable() {
        movement = playerControls.Player.Movement;
        movement.Enable();
        shield = playerControls.Player.Shield;
        shield.Enable();
    }
    private void OnDisable() {
        shield.started -= HoldingShield;
        shield.canceled -= ReleasingShield;
        shield.performed -= UseShield;
        movement.performed -= Move;
    }
    public void DisableControls() {
        movement.Disable();
        shield.Disable();
    }
    public void Move(InputAction.CallbackContext context) {
        currMoveDir = movement.ReadValue<Vector2>();
        OnMovement.Invoke(currMoveDir);
    }
    public void UseShield(InputAction.CallbackContext context)
    {
      
    }
    public void HoldingShield(InputAction.CallbackContext context) {
      
        OnShieldPressed.Invoke();
    }
    public void ReleasingShield(InputAction.CallbackContext context) {
        
        OnShieldReleased.Invoke();
    }
  
}
