using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxBase : MonoBehaviour
{
    public string title;
    public string content;
    private GameObject titleObj;
    private GameObject contentObj;
    private GameObject btnObj;
    public GameObject rootObj;
    public GameObject mainCamera;
    private bool hasActivated;

    private void Start()
    {
        titleObj = rootObj.transform.Find("Title").gameObject;
        contentObj = rootObj.transform.Find("Content").gameObject;
        btnObj = rootObj.transform.Find("Button").gameObject;
    }

    public virtual void addExtra()
    { }

    public virtual void removeExtra()
    { }

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
        mainCamera.GetComponent<CameraTools>().stopCamera();
        rootObj.SetActive(true);
        titleObj.GetComponent<Text>().text = title;
        contentObj.GetComponent<Text>().text = content;
        btnObj.GetComponent<Button>().onClick.AddListener(HideDialog);
        hasActivated = true;
        addExtra();
    }

    public void HideDialog()
    {
        mainCamera.GetComponent<CameraTools>().stopCamera();
        rootObj.SetActive(false);
        btnObj.GetComponent<Button>().onClick.RemoveAllListeners();
        removeExtra();
    }
}
