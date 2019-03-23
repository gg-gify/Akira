using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseCharacterSceneConttroler : MonoBehaviour
{
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
