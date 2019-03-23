using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerSceneController : MonoBehaviour
{
    private PlayerType playerType;

    private void Start()
    {
        string playerTypeStr = PlayerPrefs.GetString("playerType");
        if(playerTypeStr == "SmallPlayer")
        {
            playerType = PlayerType.SmallPlayer;
        }
        else if (playerTypeStr == "BigPlayer")
        {
            playerType = PlayerType.BigPlayer;
        }
        else
        {
            Debug.LogError("PlayerTyper not set!");
        }
    }
    public void ChoseCharacter(int playerType)
    {
        if(playerType == 0)
        {
            PlayerPrefs.SetString("playerType", "SmallPlayer");
        }
        else
        {
            PlayerPrefs.SetString("playerType", "BigPlayer");
        }

        SceneManager.LoadScene("ControllerScene");
    }
}

public enum PlayerType
{
    SmallPlayer,
    BigPlayer
}