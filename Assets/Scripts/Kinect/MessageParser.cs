using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class MessageParser : MonoBehaviour
{
    public delegate void DetectedGesture(string gesture);
    public event DetectedGesture onDetectedGesture;

    private float timeToWaitTillNextGesture;
    private Dictionary<string, object> lastMessage;
    private Dictionary<string, object> skeleton;
    private float timeSinceLastGesture;
    public static MessageParser instance;
    public float waitTillNextGesture = 1.5f;


    private void Start()
    {
        if (instance == null)
            instance = this;

        timeToWaitTillNextGesture = waitTillNextGesture;
        WebsocketConnection.instance.onNewOSCMessage += ParseNewMessages;
    }

    public bool TryGetPosOfSkeletonFeature(string feature, out Vector3 position)
    {
        position = new Vector3();
        var success = skeleton.TryGetValue(feature, out var featureObject);
        if (!success)
        {
            return false;
        }
        var parsedFeature = JsonConvert.DeserializeObject<Dictionary<string, object>>(featureObject.ToString());
        var pos = JsonConvert.DeserializeObject<Dictionary<string, float>>(parsedFeature["position"].ToString());
        position = new Vector3(pos["x"], pos["y"], pos["z"]);
        return true;
    }

    private void ParseNewMessages(ArrayList newMessages)
    {
        if (newMessages.Count > 1)
        {
            Debug.Log("received more than 1 message: \n" + String.Join("\n Next Message: \n", newMessages));
        }
        var lastNewMessage = newMessages[newMessages.Count - 1].ToString().Substring(8);
        var parsedMessage = JsonConvert.DeserializeObject<Dictionary<string, object>>(lastNewMessage);
        lastMessage = parsedMessage;
        var kinectData = JsonConvert.DeserializeObject<Dictionary<string, object>>(parsedMessage["kinectData"].ToString());
        var trackedBodies = JsonConvert.DeserializeObject<object[]>(kinectData["trackedBodies"].ToString());
        //check for gesture
        var output4 = JsonConvert.DeserializeObject<Dictionary<string, object>>(trackedBodies[0].ToString());
        var trackedGesture = output4["trackedGesture"].ToString();
        if (trackedGesture != null)
        {
            if(trackedGesture != "")
            {
                Debug.Log("Detected " + trackedGesture);
                if (Time.time > timeSinceLastGesture + timeToWaitTillNextGesture)
                {
                    timeSinceLastGesture = Time.time;
                    onDetectedGesture(trackedGesture);
                }
            }
        }
        //parse skeleton Data
        var firstBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(trackedBodies[0].ToString());
        var trackedSkeleton = JsonConvert.DeserializeObject<Dictionary<string, object>>(firstBody["trackedSkeleton"].ToString());
        skeleton = trackedSkeleton;
    }
}