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

    [SerializeField] private float timeBetweenMoves = 0.1f;
    private bool moveHeld = false;
    private bool alreadyHeld = false;
    private Vector2 currMoveDir;

    [HideInInspector] public UnityEvent OnShieldUse;
    [HideInInspector] public UnityEvent<Vector2> OnMovement;
    [HideInInspector] public UnityEvent OnMoving; // Use this for audio stuf
    private void Awake() {
        playerControls = new PlayerControls();
    }
    private void Update() {
        shield.performed += UseShield;
        movement.canceled += ReleasingMove;
        movement.started += HoldingMove;
        movement.performed += Move;
    }
    private void OnEnable() {
        movement = playerControls.Player.Movement;
        movement.Enable();
        shield = playerControls.Player.Shield;
        shield.Enable();
    }
    private void OnDisable() {
        shield.performed -= UseShield;
        movement.canceled -= ReleasingMove;
        movement.started -= HoldingMove;
        movement.performed -= Move;
    }
    public void DisableControls() {
        movement.Disable();
        shield.Disable();
    }
    public void Move(InputAction.CallbackContext context) {
        if (alreadyHeld) return;
        currMoveDir = movement.ReadValue<Vector2>();
        //StartCoroutine(Moving());
        OnMovement.Invoke(currMoveDir);
        OnMoving.Invoke();
    }
    public void HoldingMove(InputAction.CallbackContext context) {
        moveHeld = true;
    }
    public void ReleasingMove(InputAction.CallbackContext context) {
        moveHeld = false;
    }
    public void UseShield(InputAction.CallbackContext context) {
        Debug.Log("Used Shield");
        OnShieldUse.Invoke();
    }
    // to allow click and hold movement
    private IEnumerator Moving() {
        while (moveHeld) {
            alreadyHeld = true;
            OnMovement.Invoke(currMoveDir);
            OnMoving.Invoke();
            yield return new WaitForSeconds(timeBetweenMoves);
        }
        alreadyHeld = false;
    }
}
