using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    CalculatePosition joystickPos;
    public BoxCollider2D area;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        joystickPos = FindObjectOfType<CalculatePosition>();
    }

    void Update()
    {
        movementDirection = new Vector2(joystickPos.axisX.x, joystickPos.axisY.y);
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;

        // get the current position
        Vector3 clampedPosition = transform.position;
        // limit the x and y positions to be between the area's min and max x and y.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, area.bounds.min.x, area.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, area.bounds.min.y, area.bounds.max.y);
        // apply the clamped position
        transform.position = clampedPosition;
    }
}
