using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLoot : MonoBehaviour
{
    [SerializeField]
    private List<int> lootProgressionByLevel;

    private int currentLevel;
    private int currentLootProgression;
    private int progressionNeededForNextLevel;

    private void Start()
    {
        SetNextLevelLootProgression();
    }

    public void AddProgression(int amount = 1)
    {
        currentLootProgression += amount;
        UIManager.Instance.SetProgression((float)currentLootProgression / (float)progressionNeededForNextLevel);

        if(currentLootProgression >= progressionNeededForNextLevel)
        {
            UIManager.Instance.SetProgression(1);
            currentLootProgression = 0;
            currentLevel++;
            SetNextLevelLootProgression();
        }
    }

    public void SetProgression(int amount)
    {
        currentLootProgression = amount;
    }

    public void ResetProgression()
    {
        SetProgression(0);
    }

    private void SetNextLevelLootProgression()
    {
        if (lootProgressionByLevel.Count >= currentLevel + 1)
            progressionNeededForNextLevel = lootProgressionByLevel[currentLevel];
        else
            progressionNeededForNextLevel = lootProgressionByLevel[lootProgressionByLevel.Count - 1];
    }
}
