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
        SaveController.Load(highScoreData);
        highScoreMenu.Show(highScoreData);
    }

    public void OnExitApplicationClick()
    {
        Application.Quit();
    }

    public void OnBackToMainMenuClicked()
    {

    }

    public void OnResetHighScoresClicked()
    {
        SaveController.DeleteSaveFile();
        highScoreData = new HighScoreData();
        OnHighScoresButtonClicked();
    }
}
