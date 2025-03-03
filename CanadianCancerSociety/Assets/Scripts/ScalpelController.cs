using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalpelController : MonoBehaviour
{
    public Animator anim;

    public GameObject redCurser;
    public GameObject bloodParticle;

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

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isPressing", true);
            redCurser.SetActive(false);
            bloodParticle.SetActive(true);
        }
        else
        {
            anim.SetBool("isPressing", false);
            redCurser.SetActive(true);
            bloodParticle.SetActive(false);
        }
    }
}
