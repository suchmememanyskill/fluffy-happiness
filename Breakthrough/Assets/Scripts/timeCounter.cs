using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCounter : MonoBehaviour
{
    public float timePerSecond;
    private double timeHeld = 0;
    public GameObject ship;

    void FixedUpdate()
    {
        if (!ship.GetComponent<Controller>().hasLiftedOff)
            return;

        timeHeld += Time.fixedDeltaTime * timePerSecond;
        long local = (long)timeHeld;
        long days = local / (60 * 60 * 24);
        local -= 60 * 60 * 24 * days;
        long hours = local / (60 * 60);
        local -= 60 * 60 * hours;
        long minutes = local / 60;
        local -= minutes * 60;


        GetComponent<Text>().text = $"Time since departure: {days} days, {hours} hours, {minutes} minutes, {local} seconds\n\nScore: {ship.GetComponent<Controller>().score}";
    }
}
