using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField colorRedInputField;
    [SerializeField] private InputField colorGreenInputField;
    [SerializeField] private InputField colorBlueInputField;
    [SerializeField] private Image imageDisplay;

    private void Start()
    {
        if(Load())
        {
            SetColorDisplay();
        }
        else
        {
            colorRedInputField.text = Random.Range(0, 256).ToString();
            colorGreenInputField.text = Random.Range(0, 256).ToString();
            colorBlueInputField.text = Random.Range(0, 256).ToString();
            SetColorDisplay();
        }
    }

    private bool Load()
    {
        string playerName = PlayerPrefs.GetString("playerName", "none");
        if (playerName != "none")
        {
            nameInputField.text = playerName;
            Debug.Log(PlayerPrefs.GetInt("colorRed"));
            Debug.Log(PlayerPrefs.GetInt("colorGreen"));
            Debug.Log(PlayerPrefs.GetInt("colorBlue"));
            colorRedInputField.text = PlayerPrefs.GetInt("colorRed").ToString();
            colorGreenInputField.text = PlayerPrefs.GetInt("colorGreen").ToString();
            colorBlueInputField.text = PlayerPrefs.GetInt("colorBlue").ToString();
            return true;
        }
        return false;
    }

    public void Save()
    {
        PlayerPrefs.SetString("playerName", nameInputField.text);
        PlayerPrefs.SetInt("colorRed", (int.Parse(colorRedInputField.text)));
        PlayerPrefs.SetInt("colorGreen", (int.Parse(colorGreenInputField.text)));
        PlayerPrefs.SetInt("colorBlue", (int.Parse(colorBlueInputField.text)));
    }

    public void SetColorDisplay()
    {
        int red = int.Parse(colorRedInputField.text);
        int green = int.Parse(colorGreenInputField.text);
        int blue = int.Parse(colorBlueInputField.text);
        imageDisplay.color = new Color(red, green, blue, 255);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("AuthenticationScene");
    }
}
