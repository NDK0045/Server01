using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;
using WebSocketSharp;
public class MyWebSocketServer : MonoBehaviour
{
    private WebSocketServer wssvSend;
    private WebSocketServer wssvReceive;

    public void StartServer()
    {
        // Initialize the WebSocket servers for send and receive
        wssvSend = new WebSocketServer("ws://localhost:8000");
        wssvReceive = new WebSocketServer("ws://localhost:8001");

        wssvSend.AddWebSocketService<SendBehavior>("/Send");
        wssvReceive.AddWebSocketService<ReceiveBehavior>("/Receive");

        wssvSend.Start();
        wssvReceive.Start();

        Debug.Log("WebSocket servers started on ports 8000 (send) and 8001 (receive)");
    }

    public void StopServer()
    {
        wssvSend?.Stop();
        wssvReceive?.Stop();
    }

    private void OnApplicationQuit()
    {
        StopServer();
    }

    private void Start()
    {
        StartServer();
    }

}

public class SendBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Send("Received: " + e.Data);
        Debug.Log("Sent acknowledgement for: " + e.Data);
    }
}

public class ReceiveBehavior : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Debug.Log("Received data: " + e.Data);
    }
}
