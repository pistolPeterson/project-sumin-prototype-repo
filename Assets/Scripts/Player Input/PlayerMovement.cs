using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * :)
     * UP -> W / Up Arrow
     * DOWN -> S / Down Arrow
     * Click or Hold
     */
    [SerializeField] private InputManager inputManager;

    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private float upperPositionConstraint = 8f;
    [SerializeField] private float lowerPositionConstraint = -8f;

    private Vector2 moveInput;

    private void Start()
    {
        inputManager.OnMovement.AddListener(MovePlayer);
    }

    public void MovePlayer(Vector2 inputDirection)
    {
        ClampPosition(); 
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

    private void ClampPosition()
    {
       // transform.y = Mathf.clamp(-positionConstraint, positionConstraint);
    }

    // In case we wanna use this for a debuff later:
    public void MovePlayerUp(float distanceToMove)
    {
        if (transform.position.y + moveDistance > upperPositionConstraint)
        {
            transform.position = new Vector3(transform.position.x, upperPositionConstraint, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMove,
                transform.position.z);
        }
    }

    
    public void MovePlayerDown(float distanceToMove)
    {
        if (transform.position.y - moveDistance < lowerPositionConstraint)
        {
            transform.position = new Vector3(transform.position.x, lowerPositionConstraint, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - distanceToMove,
                transform.position.z);
        }
    }
}