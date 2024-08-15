using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledProjectile : Projectile
{
   
    [SerializeField] private float xOffsetToAngle = 3;
    [field: SerializeField] public Direction angledDirection { get; set; }
    [SerializeField] private GameObject objToRotate;
    private Transform playerTransform;
    private Vector2 moveDirection;

    private void Start() {
        playerTransform = FindObjectOfType<PlayerMovement>()?.transform;
        Debug.Log(playerTransform);
        SetMoveDirection(Direction.LEFT); // default
    }
    public override void MoveProjectile() {
        if (transform.position.x <= playerTransform?.position.x + xOffsetToAngle) {
            SetMoveDirection(angledDirection);
        }
        else {
            SetMoveDirection(Direction.LEFT);
        }
        rb.velocity = moveDirection * projectileSpeed * Time.fixedDeltaTime;
    }
    public void SetAngledDirection(Direction dir) {
        angledDirection = dir;
    }
    private void SetMoveDirection(Direction dir) {
        switch (dir) {
            case Direction.LEFT:
                moveDirection = new Vector2(-120f, 0f);
                break;
            case Direction.UP_LEFT:
                moveDirection = (Vector2.up + Vector2.left).normalized * 120f;
                break;
            case Direction.DOWN_LEFT:
                moveDirection = (Vector2.down + Vector2.left).normalized * 120f;
                break;
        }
        ChangeRotationOnDirection(dir);
    }
    public void ChangeRotationOnDirection(Direction dir) {
        switch (dir) {
            case Direction.DOWN_LEFT:
                objToRotate.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                break;
            case Direction.LEFT:
                objToRotate.transform.rotation = Quaternion.identity;
                break;
            case Direction.UP_LEFT:
                objToRotate.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -45f));
                break;
        }
    }
    
}
public enum Direction {
    UP_LEFT,
    DOWN_LEFT,
    LEFT
}
