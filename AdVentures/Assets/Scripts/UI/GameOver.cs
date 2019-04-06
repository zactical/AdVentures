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
    }


    public void OnRestartGameClick()
    {
        if (string.IsNullOrEmpty(nameInputField.text) == false)
            GameManager.Instance.SaveCurrentScore(nameInputField.text);
        GameManager.Instance.ReloadScene();
    }

    public void OnExitGameClick()
    {

    }
}
