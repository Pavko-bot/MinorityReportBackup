using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKinect : MonoBehaviour
{

    public AnimationCurve sampleCurve;
    public bool resetHighestY;
    [SerializeField]
    private float highestY;

    public delegate void DetectedGesture();
    public event DetectedGesture onDetectedGestureUp = delegate { };
    public event DetectedGesture onDetectedGestureDown = delegate { };
    public event DetectedGesture onDetectedGestureLeft = delegate { };
    public event DetectedGesture onDetectedGestureRight = delegate { };


    private AnimationCurve inverseSampleCurve;

    void Start()
    {
        MessageParser.instance.onDetectedGesture += TriggerEvent;

        //create inverse sampleCurve
        inverseSampleCurve = new AnimationCurve();
        for (int i = 0; i < sampleCurve.length; i++)
        {
            Keyframe inverseKey = new Keyframe(sampleCurve.keys[i].value, sampleCurve.keys[i].time);
            inverseSampleCurve.AddKey(inverseKey);
        }
    }
    void TriggerEvent(string gesture)
    {
        gesture = gesture.ToLower();
        Debug.Log("GESTURE : " + gesture);
        if (gesture.StartsWith("swipeup"))
        {
            onDetectedGestureUp();
        }
        else if (gesture.StartsWith("swipedown"))
        {
            onDetectedGestureDown();
        }
        else if (gesture.StartsWith("swipeleft"))
        {
            onDetectedGestureLeft();
        }
        else if (gesture.StartsWith("swiperight"))
        {
            onDetectedGestureRight();
        }
        else
        {
            Debug.Log("Detected unknown gesture: " + gesture);
        }
    }

    //public float getYMapped()
    //{
    //    return GetHighestYOfHandPositionMapped();
    //}
    //
    //private float GetHighestYOfHandPositionMapped()
    //{
    //    var highestHandPos = GetHighestHandPosition();
    //    if (highestY < highestHandPos.y || resetHighestY)
    //    {
    //        highestY = highestHandPos.y;
    //        resetHighestY = false;
    //    }
    //    var value = inverseSampleCurve.Evaluate(highestHandPos.y);
    //    return value;
    //}
    //
    //private Vector3 GetHighestHandPosition()
    //{
    //    var successLeft = messageParser.TryGetPosOfSkeletonFeature("handLeft", out var positionHandLeft);
    //    var successRight = messageParser.TryGetPosOfSkeletonFeature("handRight", out var positionHandRight);
    //    if(!successLeft && !successRight)
    //    {
    //        return new Vector3();
    //    }
    //    else if (!successLeft)
    //    {
    //        return positionHandRight;
    //    }
    //    else if (!successRight)
    //    {
    //        return positionHandLeft;
    //    }
    //    else
    //    {
    //        return positionHandLeft.y > positionHandRight.y ? positionHandLeft : positionHandRight;
    //    }
    //}
}