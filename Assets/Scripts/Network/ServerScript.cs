using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ServerScript : MonoBehaviour
{
    private MultiplayerAkiraInput inputPlayer1;
    private MultiplayerAkiraInput inputPlayer2;
    private bool oldValueA;
    private bool oldValueB;
    private bool oldValueStart;
    private bool oldValuePerfil;
    private GameController gameController;
    private GameMenuController gameMenuController;

    void Start()
    {
        NetworkServer.Listen(7000);
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
        gameController = GetComponent<GameController>();
        gameMenuController = GetComponent<GameMenuController>();
        GetPlayerHexIP(1);
    }

    void OnGUI()
    {

        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 25, 100, 20), "Connected" + NetworkServer.connections.Count);
    }

    public static ServerScriptData GetServerData(string hexCode)
    {
        string pedacoIP = hexCode.Substring(0, 2);
        string ipString = int.Parse(pedacoIP, System.Globalization.NumberStyles.HexNumber).ToString();

        pedacoIP = hexCode.Substring(2, 2);
        ipString += "." + int.Parse(pedacoIP, System.Globalization.NumberStyles.HexNumber).ToString();

        pedacoIP = hexCode.Substring(4, 2);
        ipString += "." + int.Parse(pedacoIP, System.Globalization.NumberStyles.HexNumber).ToString();

        pedacoIP = hexCode.Substring(6, 2);
        ipString += "." + int.Parse(pedacoIP, System.Globalization.NumberStyles.HexNumber).ToString();

        return new ServerScriptData(ipString, int.Parse(hexCode.Substring(8, 1)));
    }


    public static string GetPlayerHexIP(int player)
    {
        string nome = Dns.GetHostName();

        IPAddress[] ip = Dns.GetHostAddresses(nome);
        string ipF = ip[1].ToString();

        string[] values = ipF.Split('.');

        string final = "";
        for (int i = 0; i < 4; i++)
        {
            int valueInt = int.Parse(values[i]);



            string hexValue = valueInt.ToString("X2");

            final += hexValue;
        }
        Debug.Log(final);

        return final + player;
    }



    //horizontal|vertical|A|B|start|perfil
    private void ServerReceiveMessage(NetworkMessage message)
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
        int idPlayer = int.Parse(values[6]);

        if (gameMenuController != null)
        {
            if (idPlayer == 1)
            gameMenuController.ConnectPlayer1();
            else
            gameMenuController.ConnectPlayer2();
        }

        if (inputPlayer1 != null && inputPlayer2 != null)
        {
            if (idPlayer == 1)
            {
                inputPlayer1.SetPad(new Vector2(horizontal, vertical));
                inputPlayer1.SetButtonA(ConvertInt2BoolButton(valueA, ref oldValueA));
                inputPlayer1.SetButtonB(ConvertInt2BoolButton(valueB, ref oldValueB));
            }
            else
            {

                inputPlayer2.SetPad(new Vector2(horizontal, vertical));
                inputPlayer2.SetButtonA(ConvertInt2BoolButton(valueA, ref oldValueA));
                inputPlayer2.SetButtonB(ConvertInt2BoolButton(valueB, ref oldValueB));

            }

        }
        else if (gameController != null)
        {
            inputPlayer1 = gameController.GetPlayer1().GetInput();
            inputPlayer2 = gameController.GetPlayer2().GetInput();
        }

    }

    private bool ConvertInt2BoolButton(int button, ref bool oldButton)
    {
        if (button > 0)
        {
            oldButton = true;
            return true;
        }
        else if (button < 0)
        {
            oldButton = false;
            return false;
        }
        return oldButton;
    }



}

public class ServerScriptData
{
    public int player;
    public string ip;

    public ServerScriptData(string ip, int player)
    {
        this.ip = ip;
        this.player = player;

    }
}
