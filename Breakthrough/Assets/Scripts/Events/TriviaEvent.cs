﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
public class TriviaList
{
    //public List<Trivia> triviaQuestions { get; set; }
    public List<Trivia> triviaQuestions;
}

public class TriviaEvent : MonoBehaviour
{
    private GameObject titleObj;
    private GameObject contentObj;
    private List<GameObject> btnObj;
    public GameObject rootObj;
    public GameObject mainCamera;
    private bool hasActivated;
    public int amountOfTrivia;
    TriviaList data;
    Trivia currentTrivia;

    // Start is called before the first frame update
    void Start()
    {
        titleObj = rootObj.transform.Find("Title").gameObject;
        contentObj = rootObj.transform.Find("Content").gameObject;
        btnObj = new List<GameObject>();
        btnObj.Add(rootObj.transform.Find("btn1").gameObject);
        btnObj.Add(rootObj.transform.Find("btn2").gameObject);
        btnObj.Add(rootObj.transform.Find("btn3").gameObject);
        btnObj.Add(rootObj.transform.Find("btn4").gameObject);
        btnObj.Add(rootObj.transform.Find("btn5").gameObject);
        readJsonData();
    }

    void readJsonData()
    {
        string json = File.ReadAllText(Application.dataPath + "/Data/trivia.json");
        data = JsonUtility.FromJson<TriviaList>(json);
        Debug.Log(json);
        Debug.Log(data);
        Debug.Log(data.triviaQuestions.Count);
    }

    private void setRandomTrivia()
    {
        currentTrivia = data.triviaQuestions[UnityEngine.Random.Range(0, data.triviaQuestions.Count)];
        data.triviaQuestions.Remove(currentTrivia);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasActivated)
            return;

        if (col.GetComponent<Controller>() != null)
        {
            StartTrivia();
        }
    }

    void StartTrivia()
    {
        hasActivated = true;
        mainCamera.GetComponent<CameraTools>().stopCamera();
        rootObj.SetActive(true);

        btnObj[0].GetComponent<Button>().onClick.AddListener(btn1Func);
        btnObj[1].GetComponent<Button>().onClick.AddListener(btn2Func);
        btnObj[2].GetComponent<Button>().onClick.AddListener(btn3Func);
        btnObj[3].GetComponent<Button>().onClick.AddListener(btn4Func);
        btnObj[4].GetComponent<Button>().onClick.AddListener(SetNewTrivia);

        SetNewTrivia();
    }

    void btn1Func() => answer(1);
    void btn2Func() => answer(2);
    void btn3Func() => answer(3);
    void btn4Func() => answer(4);

    void SetNewTrivia()
    {
        swapButtons(true);
        if (amountOfTrivia <= 0)
        {
            StopTrivia();
            return;
        }

        setRandomTrivia();
        titleObj.GetComponent<Text>().text = currentTrivia.title;
        contentObj.GetComponent<Text>().text = currentTrivia.question;

        for (int i = 0; i < btnObj.Count - 1; i++)
        {
            btnObj[i].transform.Find("Text").GetComponent<Text>().text = currentTrivia.answers[i];
        }

        amountOfTrivia--;
    }

    void swapButtons(bool triviaMode)
    {
        for (int i = 0; i < btnObj.Count - 1; i++)
        {
            btnObj[i].SetActive(triviaMode);
        }
        btnObj[4].SetActive(!triviaMode);
    }

    void answer(int answer)
    {
        string title = (answer == currentTrivia.correctAnswer) ? "Correct! " : "Incorrect! ";

        title += currentTrivia.answers[currentTrivia.correctAnswer - 1] + " is the correct answer";
        titleObj.GetComponent<Text>().text = title;
        contentObj.GetComponent<Text>().text = currentTrivia.explanation;
        swapButtons(false);
    }

    void StopTrivia()
    {
        mainCamera.GetComponent<CameraTools>().startCamera();
        rootObj.SetActive(false);
        foreach (GameObject btn in btnObj)
            btn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
