using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using WebSocketSharpCore;
/*//using WebSocketSharpCore.Server;
using WebSocketSharpCore;*/
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket wsSend;
    private WebSocket wsReceive;

    private void Start()
    {
        wsSend = new WebSocket("ws://localhost:8000/Send");
        wsReceive = new WebSocket("ws://localhost:8001/Receive");

        wsSend.OnMessage += (sender, e) =>
        {
            Debug.Log("Message from server (send): " + e.Data);
        };

        wsReceive.OnMessage += (sender, e) =>
        {
            Debug.Log("Message from server (receive): " + e.Data);
        };

        wsSend.Connect();
        wsReceive.Connect();
    }

    public void SendString(string message)
    {
        wsSend.Send(message);
        Debug.Log("Sent: " + message);
    }

    private void OnDestroy()
    {
        wsSend?.Close();
        wsReceive?.Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendString("Hello from Client");
        }
    }

}
