using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Image player1StatusImage;
    [SerializeField] private Text player1IpText;
    [SerializeField] private Image player2StatusImage;
    [SerializeField] private Text player2IpText;

    private bool player1Connected;
    private bool player2Connected;
    private ServerScript serverScript;

    private void Start()
    {
        player1IpText.text = ServerScript.GetPlayerHexIP(1);
        player2IpText.text = ServerScript.GetPlayerHexIP(2);
        serverScript = GetComponent<ServerScript>();
    }

    private void Update()
    {
        if (player1Connected && player2Connected)
        {
            playButton.interactable = true;
        }
    }

    public void ConnectPlayer1()
    {
        player1StatusImage.color = Color.green;
        player1IpText.text = "Connected";
    }

    public void ConnectPlayer2()
    {
        player2StatusImage.color = Color.green;
        player2IpText.text = "Connected";
    }
}
