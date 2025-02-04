using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    CalculatePosition joystickPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        joystickPos = FindObjectOfType<CalculatePosition>();
    }

    void Update()
    {
        movementDirection = new Vector2(joystickPos.axisX.x + joystickPos.axisY.x,
            joystickPos.axisY.y + joystickPos.axisX.y);
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
}
