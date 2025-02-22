using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  
    private float moveInput;      
    private Rigidbody2D rb;       
    private SpriteRenderer spriteRenderer;

    public bool inDialog = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!inDialog)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput != 0)
            {
                spriteRenderer.flipX = moveInput < 0;
            }
        }
    }
}
