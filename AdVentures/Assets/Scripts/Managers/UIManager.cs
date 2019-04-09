using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField]
    private AlertPanel alertPanel;
    [SerializeField]
    private GameOver gameOverScreen;
    [SerializeField]
    private ScoreText scoreText;
    [SerializeField]
    private HighScoreMenu highScoreMenu;
    [SerializeField]
    private ProgressionMeter progressionMeter;
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private InstructionPanel instructionPanel;
    [SerializeField]
    private PopUpActionText popupActionText;

    private Vector3 alertPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        alertPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, .25f, 0));
        alertPosition.z = 0;
        SetProgression(0);
        var alertPool = Pool.GetPool(alertPanel, alertPosition);
        alertPool.WarmPool();

        StartCoroutine(HideInstructionPanelAfterSeconds(2));
    }

    public void SetAlert(LootType loot)
    {
        var newAlert = alertPanel.Get<AlertPanel>(alertPosition, Quaternion.identity);
        newAlert.TriggerAlert(loot);
    }

    public void ShowGameOverScreen(HighScoreData highScores, int score, string text)
    {
        highScoreMenu.Show(highScores);
        gameOverScreen.Show(score, text);
    }

    private IEnumerator HideInstructionPanelAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        instructionPanel.Toggle();
    }

    public void UpdateScore(int newScore)
    {
        scoreText.UpdateScore(newScore);
    }

    public void UpdateWaveText(int waveNumber)
    {
        waveText.text = string.Format("Wave: {0}", waveNumber);
    }

    public void SetProgression(float amount)
    {
        progressionMeter.UpdateAmount(amount);
    }

    public void CreateActionText(Vector3 position, int amount)
    {
        popupActionText.Get<PopUpActionText>(position, Quaternion.identity).SetScoreText(amount);
    }
}
