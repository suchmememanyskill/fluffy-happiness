using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxBase : MonoBehaviour
{
    public string title;
    public string content;
    public Text titleObj;
    public Text contentObj;
    public Button confirmObj;
    public GameObject rootObj;
    public GameObject mainCamera;
    private bool hasActivated;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasActivated)
            return;

        if (col.GetComponent<Controller>() != null)
        {
            ShowDialog();
        }
    }

    public void ShowDialog()
    {
        mainCamera.GetComponent<Follower>().enabled = false;
        rootObj.SetActive(true);
        titleObj.text = title;
        contentObj.text = content;
        confirmObj.onClick.AddListener(HideDialog);
        hasActivated = true;
    }

    public void HideDialog()
    {
        mainCamera.GetComponent<Follower>().enabled = true;
        rootObj.SetActive(false);
        confirmObj.onClick.RemoveAllListeners();
    }
}
