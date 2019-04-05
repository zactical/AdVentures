using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField]
    private AlertPanel alertPanel;
    [SerializeField]
    private Transform alertStartingLocation;

    [SerializeField]
    private GameOver gameOverScreen;
    [SerializeField]
    private ScoreText scoreText;
    [SerializeField]
    private HighScoreMenu highScoreMenu;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        var alertPool = Pool.GetPool(alertPanel, alertStartingLocation);
        alertPool.WarmPool();        
    }

    public void SetAlert(LootType loot)
    {
        var newAlert = alertPanel.Get<AlertPanel>(alertStartingLocation.transform.position, Quaternion.identity);
        newAlert.TriggerAlert(loot);
    }

    public void ShowGameOverScreen(int score, string text)
    {
        highScoreMenu.Show();
        gameOverScreen.Show(score, text);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.UpdateScore(newScore);
    }
}
