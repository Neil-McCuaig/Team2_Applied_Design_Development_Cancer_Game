using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraFollow : MonoBehaviour
{
    private Vector3 offset;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private Vector3 target;

    private void Start()
    {
        offset = new Vector3(0f, 0f, -0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        target = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(target);
        target.z = 0f;

        Vector3 targetPosition = target + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
