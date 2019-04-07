using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField]
    private GameObject pauseMenu;

    private bool isPaused;
    private int score;
    private Player player;
    private EnemyManager enemyManager;

    private HighScoreData highScoreData = new HighScoreData();
 //   private LootManager lootManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyManager = FindObjectOfType<EnemyManager>();
    //    lootManager = FindObjectOfType<LootManager>();

        CheckAllReferencesExist();
        LoadHighScores();

        StartCoroutine(StartGameAfterDelay());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            TogglePause();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }

    public void GameOver(bool isAllLevelsFinished = false)
    {
        enemyManager.DisperseAllEnemies();
        StartCoroutine(GameOverAfterSeconds(2, isAllLevelsFinished));
    }

    public void SaveCurrentScore(string name)
    {
        highScoreData.AddRecord(new ScoreRecord(name, score));
        SaveHighScores();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
        Pool.ClearPools();
    }    

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Pool.ClearPools();
        Time.timeScale = 1;
    }

    private IEnumerator GameOverAfterSeconds(float seconds, bool isAllLevelsFinished)
    {
        yield return new WaitForSeconds(seconds);
        player.ToggleMovingEnabled(false);
        player.ToggleShootingEnabled(false);
        UIManager.Instance.ShowGameOverScreen(highScoreData, score, isAllLevelsFinished ? "You Win!" : "Game Over");
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(2);
        enemyManager.SpawnNextGroup();
    }

    private void CheckAllReferencesExist()
    {
        if (player == null)
            Debug.LogError("Could not find Player");

       // if (lootManager == null)
       //     Debug.LogError("Could not find Loot Manager");

        if (enemyManager == null)
            Debug.LogError("Could not find Enemy Manager");
    }

    private void LoadHighScores()
    {
        SaveController.Load(highScoreData);
    }

    private void SaveHighScores()
    {
        SaveController.Save(highScoreData);
    }

    public void TogglePause()
    {
        if (isPaused)
            Time.timeScale = 1;            
        else
            Time.timeScale = 0;

        pauseMenu.SetActive(!isPaused);

        isPaused = !isPaused;
    }
    
}
