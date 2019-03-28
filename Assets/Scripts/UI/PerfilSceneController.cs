using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PerfilSceneController : MonoBehaviour
{
    [SerializeField] private Image playerColor;
    [SerializeField] private Text playerName;
    [SerializeField] private Text[] levels_label_text;
    [SerializeField] private Text[] levels_score_text;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        int red = PlayerPrefs.GetInt("colorRed");
        int green = PlayerPrefs.GetInt("colorGreen");
        int blue = PlayerPrefs.GetInt("colorBlue");
        playerColor.color = new Color(red, green, blue, 255);
        playerName.text = PlayerPrefs.GetString("playerName", "none");

        for (int i = 0; i < levels_label_text.Length; i++)
        {
            int level_score = PlayerPrefs.GetInt("level_"+i+"_1", -1);
            if (level_score != -1)
            {
                levels_label_text[i].text = i+"-1";
                levels_score_text[i].text = level_score.ToString();
            }
            else
            {
                levels_label_text[i].text = "?????";
                levels_score_text[i].text = "?????";
            }
        }
    }

    public void BackScreen()
    {
        SceneManager.LoadScene("ControllerScene");
    }
}
