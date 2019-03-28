using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerSceneController : MonoBehaviour
{
    public void PerfilButton()
    {
        SceneManager.LoadScene("PerfilScene");
    }
}