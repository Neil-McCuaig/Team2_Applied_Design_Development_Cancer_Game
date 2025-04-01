using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // How long the shake lasts
    public float shakeIntensity = 0.1f; // How intense the shake is
    private Vector3 originalPosition; // Original camera position
    private float shakeTimeRemaining; // Time remaining for the shake

    void Start()
    {
        originalPosition = transform.position; // Store the original position of the camera
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // Decrease the remaining shake time
            shakeTimeRemaining -= Time.deltaTime;

            // Generate random shake position
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            transform.position = originalPosition + shakeOffset;

            // Stop shaking after the duration
            if (shakeTimeRemaining <= 0)
            {
                transform.position = originalPosition;
            }
        }
    }

    // Public method to trigger the screen shake
    public void TriggerScreenShake()
    {
        shakeTimeRemaining = shakeDuration; // Reset shake time
    }
}
