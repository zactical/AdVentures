using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour
{
    private List<HighScoreRow> rows;

    private List<HighScoreRow> Rows
    {
        get
        {
            if (rows == null)
                rows = GetComponentsInChildren<HighScoreRow>().ToList();

            return rows;
        }
    }

    public void Show()
    {
        for (int i = 0; i < Rows.Count; i++)
        {
            Rows[i].UpdateText(string.Format("{0}. Zach {1}", i, 1500 - i));
        }

        gameObject.SetActive(true);
    }
}
