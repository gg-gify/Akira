using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ClientScript : MonoBehaviour
{
    [SerializeField] private AkiraButton upPad;
    [SerializeField] private AkiraButton downPad;
    [SerializeField] private AkiraButton leftPad;
    [SerializeField] private AkiraButton rightPad;
    [SerializeField] private AkiraButton buttonA;
    [SerializeField] private AkiraButton buttonB;
    [SerializeField] private AkiraButton buttonStart;
    [SerializeField] private AkiraButton buttonPerfil;
    [SerializeField] private Text debugText;

    static NetworkClient client;
    
    void Start()
    {
        client = new NetworkClient();
    }

    public void Connect()
    {
        client.Connect("192.168.0.105", 7000);
    }

    private void Update()
    {
        float up = ((upPad.GetButton() > 0)? 1 : 0);
        float down = ((downPad.GetButton() > 0) ? -1 : 0);
        float right = ((rightPad.GetButton() > 0) ? 1 : 0);
        float left = ((leftPad.GetButton() > 0) ? -1 : 0);

        Vector2 axis = new Vector2(right + left, up + down);

        debugText.text = ClientScript.SendJoystickInfo(
            axis,
            buttonA.GetButton(),
            buttonB.GetButton(),
            buttonStart.GetButton(),
            buttonPerfil.GetButton()
            );
    }

    static public string SendJoystickInfo(Vector2 axis, int buttonA, int buttonB, int buttonStart, int buttonPerfil)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = axis.x + "|" + axis.y + "|" + buttonA + "|"+ buttonB + "|" + buttonStart + "|" + buttonPerfil;
            client.Send(888, msg);
            return msg.value;
        }
        return "";
    }
}
