
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] [Range(5f, 15f)] private float initialMovementSpeed = 10f;
   private float lerpSpeed = 5f; // Lerp speed
   public float maxMovementSpeed = 50f;
   private float currentMovementSpeed;
   private float horizontalInput;
   private float targetMovementSpeed;

   [SerializeField] private bool onPlayerInput = false; //the state if player can move the camera
   private GameObject targetToFollow;
   [SerializeField] private float followSmoothness = 0.125f;
   [SerializeField] private NodeMap nodeMap;

   private float distanceThreshold = 10.01f;


   private void Awake()
   {
       nodeMap.OnNodeProgressUpdated.AddListener(CameraOnProgressUpdate);

   }

   void Start()
   {
       currentMovementSpeed = initialMovementSpeed;
   }

   void Update()
   {
       if (onPlayerInput)
           HandlePlayerMovement();
       else
           CameraFollow();

   }

   public void SetHorizontalInput(float newInput)
   {
       horizontalInput = newInput;
   }

   private void CameraFollow()
   {
       if(!targetToFollow)
           return;
       Vector3 targetPosition = targetToFollow.transform.position;
       Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSmoothness);
       transform.position = new Vector3(smoothPosition.x, transform.position.y, transform.position.z); // Lock Y and Z axes
       float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
       if (distanceToTarget <= distanceThreshold)
       {
           // Camera is close enough to the target, enable player input
           onPlayerInput = true;
       }
   }

   private void HandlePlayerMovement()
   {
      // horizontalInput = Input.GetAxisRaw("Horizontal");

       if (horizontalInput != 0) // Check if there's input
       {
           targetMovementSpeed = maxMovementSpeed;
       }
       else
       {
           targetMovementSpeed = initialMovementSpeed;
       }

       currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, targetMovementSpeed, lerpSpeed * Time.deltaTime);
       transform.Translate(Vector3.right * horizontalInput * currentMovementSpeed * Time.deltaTime);
   }

   private void CameraOnProgressUpdate()
   {
       //set target to follow
       targetToFollow = nodeMap.GetCurrentNode();
       //switch camera follow on
       onPlayerInput = false;

      
   }
}
