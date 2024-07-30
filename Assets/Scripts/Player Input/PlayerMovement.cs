using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * :)
     * UP -> W / Up Arrow
     * DOWN -> S / Down Arrow
     * Click to move
     */
    [SerializeField] private InputManager inputManager;

    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private float positionConstraint = 8f;

    private Vector2 moveInput;

    private void Start()
    {
        inputManager.OnMovement.AddListener(MovePlayer);
    }

    public void MovePlayer(Vector2 inputDirection)
    {
        moveInput = inputDirection;
        if (moveInput == Vector2.up)
        {
            MovePlayerUp(moveDistance);
        }
        else if (moveInput == Vector2.down)
        {
            MovePlayerDown(moveDistance);
        }
    }

    private void ClampPosition(float valueToCheck)
    {
        float clampedPositionY = Mathf.Clamp(valueToCheck, -positionConstraint, positionConstraint);
        transform.position = new Vector3(transform.position.x, clampedPositionY, transform.position.z);
    }

    // In case we wanna use this for a debuff later:
    public void MovePlayerUp(float distanceToMove)
    {
        ClampPosition(transform.position.y + distanceToMove);
    }    
    public void MovePlayerDown(float distanceToMove)
    {
        ClampPosition(transform.position.y - distanceToMove);
    }
}