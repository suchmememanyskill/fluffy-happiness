using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Trivia
{
    public string title;
    public string question;
    public string explanation;
    public List<string> answers;
    public int correctAnswer;
    public int scoreAmount;
}

[Serializable]
public class TextBoxData
{
    public string title;
    public string content;
    public string buttonName;
}

[Serializable]
public class Data
{
    public List<TextBoxData> textBoxData;
    public List<Trivia> triviaData;
}

public class DataGatherer
{
    private Data data;
    public List<TextBoxData> GetTextBoxData() => data.textBoxData;

    public List<Trivia> getCopyOfTrivia()
    {
        List<Trivia> trivia = new List<Trivia>();
        foreach (Trivia t in data.triviaData)
            trivia.Add(t);
        
        return trivia;
    }

    private static DataGatherer single;

    private DataGatherer(string path)
    {
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(json);
        Debug.Log(json);
        Debug.Log(data);
        Debug.Log(data.triviaData);
    }

    public static DataGatherer Get()
    {
        if (single == null)
            single = new DataGatherer(Application.dataPath + "/Data/data.json");

        return single;
    }
}
