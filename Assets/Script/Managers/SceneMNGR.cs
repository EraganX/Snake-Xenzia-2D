using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMNGR : MonoBehaviour
{
    public void StartGame(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Gameplay");
    }

    public void RestartGame(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
