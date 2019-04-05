using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rowText;

    public void UpdateText(string text)
    {
        rowText.text = text;
    }
}
