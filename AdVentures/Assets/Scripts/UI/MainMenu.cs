using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MainButtonPanel;
    [SerializeField]
    private GameObject HighScorePanel;
    [SerializeField]
    private HighScoreMenu highScoreMenu;

    private HighScoreData highScoreData = new HighScoreData();

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnHighScoresButtonClicked()
    {
        MainButtonPanel.SetActive(false);
        HighScorePanel.SetActive(true);
        SaveController.Load(highScoreData);
        highScoreMenu.Show(highScoreData);
    }

    public void OnExitApplicationClick()
    {
        Application.Quit();
    }

    public void OnBackToMainMenuClicked()
    {        
        HighScorePanel.SetActive(false);
        MainButtonPanel.SetActive(true);
    }

    public void OnResetHighScoresClicked()
    {
        SaveController.DeleteSaveFile();
        highScoreData = new HighScoreData();
        highScoreMenu.Show(highScoreData);
    }
}
