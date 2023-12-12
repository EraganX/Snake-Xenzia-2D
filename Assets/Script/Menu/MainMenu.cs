using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject highScorePanel;
    public GameObject mainMenu;
    public Slider slider;
    public TMP_Text levelText;


    public void OpenHighScorePanel()
    {
        highScorePanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseHighScorePanel()
    {
        highScorePanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void Update()
    {
        levelText.text = "LEVEL " + Mathf.FloorToInt(slider.value+1) + "\nSpeed: " + Mathf.FloorToInt(slider.value + 5);
        PlayerScript.SetSpeed(Mathf.FloorToInt(slider.value));
    }

}
