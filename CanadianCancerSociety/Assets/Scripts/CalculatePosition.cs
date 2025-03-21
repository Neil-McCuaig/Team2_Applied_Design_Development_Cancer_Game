using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePosition : MonoBehaviour
{
    public Vector2 axisY;
    public Vector2 axisX;

    private float upPos;
    private float downPos;
    private float rightPos;
    private float leftPos;

    void Awake()
    {
        upPos = transform.localPosition.y + 0.3f;
        downPos = transform.localPosition.y - 0.3f;
        rightPos = transform.localPosition.x + 0.3f;
        leftPos = transform.localPosition.x - 0.3f;
    }

    void Update()
    {
        if (transform.localPosition.y >= upPos)
        {
            axisY = new Vector2(0, 1);
        }
        else if (transform.localPosition.y <= downPos)
        {
            axisY = new Vector2(0, -1);
        }
        else
        {
            axisY = Vector2.zero;
        }

        if (transform.localPosition.x >= rightPos)
        {
            axisX = new Vector2(1, 0);
        }
        else if (transform.localPosition.x <= leftPos)
        {
            axisX = new Vector2(-1, 0);
        }
        else
        {
            axisX = Vector2.zero;
        }
    }
}
