using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class WebsocketConnection : MonoBehaviour
{
    WebSocket websocket;
    OSC osc;
    public delegate void NewOSCMessages(ArrayList newMessages);
    public event NewOSCMessages onNewOSCMessage;


    public static WebsocketConnection instance;
    // Start is called before the first frame update
    async void Start()
    {
        if (instance == null)
            instance = this;


        Debug.Log("start socket");
        websocket = new WebSocket("ws://127.0.0.1:9912");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var newMessages = OSC.PacketToOscMessages(bytes, bytes.Length);
            onNewOSCMessage(newMessages);
        };
        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket!.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}