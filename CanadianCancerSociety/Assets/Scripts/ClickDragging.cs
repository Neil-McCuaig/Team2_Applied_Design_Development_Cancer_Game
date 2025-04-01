using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragging : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    Vector3 orginalPosition;

    public BoxCollider2D area;

    public GameObject tutorialIntro;

    void Start()
    {
        orginalPosition = transform.position;
    }

    void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // get the current position
            Vector3 clampedPosition = transform.position;
            // limit the x and y positions to be between the area's min and max x and y.
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, area.bounds.min.x, area.bounds.max.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, area.bounds.min.y, area.bounds.max.y);
            // apply the clamped position
            transform.position = clampedPosition;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;

        tutorialIntro.SetActive(false);
    }

    private void OnMouseUp() 
    {
        dragging = false;
        transform.position = orginalPosition;
    }
}
