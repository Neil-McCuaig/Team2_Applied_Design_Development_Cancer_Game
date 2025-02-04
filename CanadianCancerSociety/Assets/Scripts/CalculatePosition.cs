using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePosition : MonoBehaviour
{
    public Vector2 axisY;
    public Vector2 axisX;

    void Update()
    {
        if (transform.localPosition.y >= -1.8f)
        {
            axisY = new Vector2(0, 1);
        }
        else if (transform.localPosition.y <= -3.9f)
        {
            axisY = new Vector2(0, -1);
        }
        else
        {
            axisY = Vector2.zero;
        }

        if (transform.localPosition.x >= 7f)
        {
            axisX = new Vector2(1, 0);
        }
        else if (transform.localPosition.x <= 5.2f)
        {
            axisX = new Vector2(-1, 0);
        }
        else
        {
            axisX = Vector2.zero;
        }
    }
}
