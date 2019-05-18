using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

[RequireComponent(typeof(GameController))]
public class ServerScript : MonoBehaviour
{
    private MultiplayerAkiraInput input;
    private bool oldValueA;
    private bool oldValueB;
    private bool oldValueStart;
    private bool oldValuePerfil;
    private GameController gameController;

    void Start()
    {
        NetworkServer.Listen(7000);
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
        gameController = GetComponent<GameController>();
    }

    void OnGUI()
    {
        string nome = Dns.GetHostName();

        IPAddress[] ip = Dns.GetHostAddresses(nome);

        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ip[1].ToString());
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 25, 100, 20), "Connected" + NetworkServer.connections.Count);

    }


    //horizontal|vertical|A|B|start|perfil
    private void ServerReceiveMessage(NetworkMessage message)
    {
        if (input != null)
        {
            StringMessage msg = new StringMessage();
            msg.value = message.ReadMessage<StringMessage>().value;

            string[] values = msg.value.Split('|');
            float horizontal = float.Parse(values[0]);
            float vertical = float.Parse(values[1]);
            int valueA = int.Parse(values[2]);
            int valueB = int.Parse(values[3]);
            int valueStart = int.Parse(values[4]);
            int ValuePerfil = int.Parse(values[5]);

            input.SetPad(new Vector2(horizontal, vertical));
            input.SetButtonA(ConvertInt2BoolButton(valueA, ref oldValueA));
            input.SetButtonB(ConvertInt2BoolButton(valueB, ref oldValueB));
        }
        else
        {
            input = gameController.GetPlayer1().GetInput();
        }

    }

    private bool ConvertInt2BoolButton(int button, ref bool oldButton)
    {
        if(button > 0)
        {
            oldButton = true;
            return true;
        }
        else if(button < 0)
        {
            oldButton = false;
            return false;
        }
        return oldButton;
    }

   
    
}
