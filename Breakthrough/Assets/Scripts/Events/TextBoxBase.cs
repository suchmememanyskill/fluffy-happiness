using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxBase : MonoBehaviour
{
    public int textIndex;
    private GameObject titleObj;
    private GameObject contentObj;
    private GameObject btnObj;
    public GameObject rootObj;
    public GameObject mainCamera;
    private bool hasActivated;
    private TextBoxData textBox;

    private void Start()
    {
        titleObj = rootObj.transform.Find("Title").gameObject;
        contentObj = rootObj.transform.Find("Content").gameObject;
        btnObj = rootObj.transform.Find("Button").gameObject;
        DataGatherer data = DataGatherer.Get();
        textBox = data.GetTextBox(textIndex);
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

        mainCamera.GetComponent<CameraTools>().stopCameraAndPlayer();
        rootObj.SetActive(true);
        titleObj.GetComponent<Text>().text = textBox.title;
        contentObj.GetComponent<Text>().text = textBox.content;
        btnObj.GetComponent<Button>().onClick.AddListener(HideDialog);
        btnObj.transform.Find("Text").GetComponent<Text>().text = textBox.buttonName;
        hasActivated = true;
        addExtra();
    }

    public void HideDialog()
    {
        mainCamera.GetComponent<CameraTools>().startCamera();
        rootObj.SetActive(false);
        btnObj.GetComponent<Button>().onClick.RemoveAllListeners();
        removeExtra();
    }
}
