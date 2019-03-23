using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationSceneConttroler : MonoBehaviour
{

    [SerializeField] private InputField inputField;

    public void Authenticate()
    {
        SceneManager.LoadScene("ChoseCharactes");
    }
}
