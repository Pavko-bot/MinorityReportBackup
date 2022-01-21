using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    public delegate void DetectedGesture();
    public event DetectedGesture onDetectedGestureUp = delegate { };
    public event DetectedGesture onDetectedGestureDown = delegate { };
    public event DetectedGesture onDetectedGestureLeft = delegate { };
    public event DetectedGesture onDetectedGestureRight = delegate { };

    private Vector3 startPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        var endPos = Input.mousePosition;
        var move = endPos - startPos;
        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            if (move.x > 0)
            {
                onDetectedGestureRight();
            }
            else
            {
                onDetectedGestureLeft();
            }
        }
        else
        {
            if (move.y > 0)
            {
                onDetectedGestureUp();
            }
            else
            {
                onDetectedGestureDown();
            }
        }
    }
}