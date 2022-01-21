using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    public delegate void DetectedGesture();
    public event DetectedGesture onDetectedGestureUp = delegate { };
    public event DetectedGesture onDetectedGestureDown = delegate { };
    public event DetectedGesture onDetectedGestureLeft = delegate { };
    public event DetectedGesture onDetectedGestureRight = delegate { };

    private void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            onDetectedGestureUp();
        }
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            onDetectedGestureDown();
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            onDetectedGestureLeft();
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            onDetectedGestureRight();
        }
    }
}