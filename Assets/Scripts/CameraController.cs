using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] [Range(5f, 15f)] private float initialMovementSpeed = 10f;
   [SerializeField] [Range(3f, 7f)]private float accelerationRate = 5f;
   private float lerpSpeed = 5f; // Lerp speed
   public float maxMovementSpeed = 50f;
   private float currentMovementSpeed;
   private float horizontalInput;
   private float targetMovementSpeed;

   void Start()
   {
       currentMovementSpeed = initialMovementSpeed;
   }

   void Update()
   {
       horizontalInput = Input.GetAxisRaw("Horizontal");

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
}
