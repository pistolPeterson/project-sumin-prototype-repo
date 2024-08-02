using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputAction movement;
    public InputAction shield;
    [Header("Player Inputs")] public PlayerControls playerControls;
    private Vector2 currMoveDir;

    [HideInInspector] public UnityEvent OnShieldUse;
    [HideInInspector] public UnityEvent<bool> OnShieldHeld;
    [HideInInspector] public UnityEvent<Vector2> OnMovement;
    
    [SerializeField] private float shieldIntervalDelay = 0.3f;
    private bool shieldHeld = false;
    private bool alreadyHeld = false;
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
    public void UseShield(InputAction.CallbackContext context) {
        if (alreadyHeld) return;
        StartCoroutine(Shielding());
    }
    public void HoldingShield(InputAction.CallbackContext context) {
        shieldHeld = true;
        OnShieldHeld.Invoke(shieldHeld);
    }
    public void ReleasingShield(InputAction.CallbackContext context) {
        shieldHeld = false;
        OnShieldHeld.Invoke(shieldHeld);
    }
    private IEnumerator Shielding() {
        while (shieldHeld) {
            alreadyHeld = true;
            OnShieldUse.Invoke();
            yield return new WaitForSeconds(shieldIntervalDelay);
        }
        alreadyHeld = false;
    }
}
