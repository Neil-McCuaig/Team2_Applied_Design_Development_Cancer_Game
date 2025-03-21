using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer line;
    private Transform[] points;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points)
    {
        line.positionCount = points.Length;
        this.points = points;
    }

    private void Update()
    {
        for(int i = 0; i < points.Length; i++) 
        {
            line.SetPosition(i, points[i].position);
        }
    }
}
