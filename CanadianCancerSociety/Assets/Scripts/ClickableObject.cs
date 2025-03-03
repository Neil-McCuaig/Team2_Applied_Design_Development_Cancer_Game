using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    // The bool value which starts as false
    public bool isClicked = false;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Method to handle mouse clicks
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            // Toggle the bool when the object is clicked
            isClicked = true;
            // Check if the SpriteRenderer exists
            if (spriteRenderer != null)
            {
                // Directly modify the alpha value of the color to 1 (fully opaque)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }
}
