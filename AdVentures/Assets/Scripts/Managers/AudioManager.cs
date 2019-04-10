using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    [SerializeField]
    private AudioSource progressionLootAudio;
    [SerializeField]
    private AudioSource upgradeLootAudio;
    [SerializeField]
    private AudioSource enemyHitAudio;
    [SerializeField]
    private AudioSource playerHitAudio;
    [SerializeField]
    private AudioSource gameOverAudio;
    [SerializeField]
    private AudioSource playerSpawnAudio;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ProgressionLoot()
    {
        progressionLootAudio.Play();
    }

    public void UpgradeLoot()
    {
        upgradeLootAudio.Play();
    }

    public void EnemyHit()
    {
        enemyHitAudio.Play();
    }

    public void GameOver()
    {
        gameOverAudio.Play();
    }

    public void PlayerSpawn()
    {
        playerSpawnAudio.Play();
    }

    public void PlayerHit()
    {
        playerHitAudio.Play();
    }

}
