using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWingsAnimation : MonoBehaviour
{
    [SerializeField] private GameObject wingsToRotate;
    [SerializeField] private bool isRotating = true;
    [SerializeField] private float rotationSpeed = -4f;
    [SerializeField] private float rotationDelay = 0.1f;
    [field: SerializeField] public float speedMultiplier { get; set; } = 1f;

    private void Start() {
        StartCoroutine(Rotation());
    }
    private IEnumerator Rotation() {
        float currentRotation = wingsToRotate.transform.rotation.z;
        while (true) {
            if (currentRotation == -180 || currentRotation == 180) {
                rotationSpeed = -rotationSpeed * speedMultiplier;
            }
            currentRotation += rotationSpeed * speedMultiplier;
            wingsToRotate.transform.rotation = Quaternion.Euler(0,0,currentRotation);
            yield return new WaitForSeconds(rotationDelay);
            yield return new WaitUntil(GetIsRotating); // allows rotation pause
        }
    }
    private bool GetIsRotating() {
        return isRotating;
    }
    
}
