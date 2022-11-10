using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject btn_start;
    [SerializeField] private TextMeshProUGUI txt_start;
    [SerializeField] private GameObject btn_exit;

    [SerializeField] private string nextSceName;

    public void StartGame()
    {
        txt_start.text = "Continue...";
        SceneManager.LoadScene(nextSceName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
