using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class HighScoreData 
{
    public List<ScoreRecord> records = new List<ScoreRecord>();

    public void AddRecord(ScoreRecord record)
    {
        records.Add(record);
    }

    public void PrepareForSave()
    {
        records = records.OrderByDescending(x => x.Score).ToList();
    }
}

[Serializable]
public class ScoreRecord
{
    public string Name;
    public int Score;

    public ScoreRecord()
    {

    }

    public ScoreRecord(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
