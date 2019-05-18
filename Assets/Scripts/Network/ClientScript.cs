using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ClientScript : MonoBehaviour
{
    [SerializeField] AkiraButton upPad;
    [SerializeField] AkiraButton downPad;
    [SerializeField] AkiraButton leftPad;
    [SerializeField] AkiraButton rightPad;
    [SerializeField] AkiraButton buttonA;
    [SerializeField] AkiraButton buttonB;
    [SerializeField] AkiraButton buttonStart;
    [SerializeField] AkiraButton buttonPerfil;

    static NetworkClient client;
    
    void Start()
    {
        client = new NetworkClient();
    }

    public void Connect()
    {
        client.Connect("192.168.0.107", 7000);
    }

    private void Update()
    {
        float up = ((upPad.GetButton() > 0)? 1 : 0);
        float down = ((downPad.GetButton() > 0) ? -1 : 0);
        float right = ((rightPad.GetButton() > 0) ? 1 : 0);
        float left = ((leftPad.GetButton() > 0) ? -1 : 0);

        Vector2 axis = new Vector2(right + left, up + down);

        ClientScript.SendJoystickInfo(
            axis,
            buttonA.GetButton(),
            buttonB.GetButton(),
            buttonStart.GetButton(),
            buttonPerfil.GetButton()
            );
    }

    static public void SendJoystickInfo(Vector2 axis, int buttonA, int buttonB, int buttonStart, int buttonPerfil)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = axis.x + "|" + axis.y + "|" + buttonA + "|"+ buttonB + "|" + buttonStart + "|" + buttonPerfil;
            client.Send(888, msg);
        }
    }
}
