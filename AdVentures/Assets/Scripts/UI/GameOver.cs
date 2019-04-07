using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TMP_InputField nameInputField;

    public void Show(int score, string text)
    {
        titleText.text = text;
        scoreText.text = string.Format("Score: {0}", score);
        gameObject.SetActive(true);
        nameInputField.Select();
        nameInputField.ActivateInputField();
    }


    public void OnRestartGameClick()
    {
        DoSaveScore();
        GameManager.Instance.ReloadScene();
    }

    public void OnExitGameClick()
    {
        DoSaveScore();
        GameManager.Instance.QuitToMainMenu();
    }

    private void DoSaveScore()
    {
        if (string.IsNullOrEmpty(nameInputField.text) == false)
            GameManager.Instance.SaveCurrentScore(nameInputField.text);
    }
}
