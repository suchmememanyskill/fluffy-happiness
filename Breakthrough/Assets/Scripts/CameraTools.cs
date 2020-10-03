using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    public GameObject ship;
    public bool stopShip;

    public void stopCamera()
    {
        GetComponent<Follower>().enabled = false;
        if (stopShip)
            ship.GetComponent<Controller>().stopShip();
    }

    public void startCamera()
    {
        GetComponent<Follower>().enabled = true;
    }
}
