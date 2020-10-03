using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadButtonPress : MonoBehaviour
{
    public void PressDeadButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
