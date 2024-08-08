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
     private InputManager inputManager;
    [SerializeField] [Range(1f, 5f)] private float moveDistance = 3f;
    [SerializeField] private float positionConstraint = 8f;
    [SerializeField] private PlayerVisual playerVisual;

    private Vector2 moveInput;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.OnMovement.AddListener(MovePlayer);
    }

    //TODO: Add VERY SMALL lerp to not make it look jerky 
    private void MovePlayer(Vector2 inputDirection)
    {
        moveInput = inputDirection;
        if (moveInput == Vector2.up)
        {
            playerVisual.PlayUp();
            MovePlayerUp(moveDistance);
        }
        else if (moveInput == Vector2.down)
        {
            playerVisual.PlayDown();
            MovePlayerDown(moveDistance);
        }
    }

    private void ClampPosition(float currentPosition)
    {
        float clampedPositionY = Mathf.Clamp(currentPosition, -positionConstraint, positionConstraint);
        transform.position = new Vector3(transform.position.x, clampedPositionY, transform.position.z);
    }

    // In case we wanna use this for a debuff later:
    private void MovePlayerUp(float distanceToMove)
    {
        ClampPosition(transform.position.y + distanceToMove);
    }    
    private void MovePlayerDown(float distanceToMove)
    {
        ClampPosition(transform.position.y - distanceToMove);
    }
}