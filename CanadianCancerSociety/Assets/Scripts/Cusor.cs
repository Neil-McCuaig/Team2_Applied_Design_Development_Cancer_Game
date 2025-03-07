using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cusor : MonoBehaviour
{
    void Awake()
    {
        // Hide the mouse cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world space 
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the z-position of the GameObject to 0 
        mousePosition.z = 0f;

        // Move the GameObject to the mouse position
        transform.position = mousePosition;
    }
}
