﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float accSpeed;
    public float turnSpeed;
    private Rigidbody2D rb2d;
    private float vertical;
    public GameObject stars;
    public float starSpeedModifier;
    public bool hasLiftedOff;
    public bool userHasControl;
    public float maxSpeed;
    public int score = 0;
    public GameObject exhaust;

    public GameObject deadUIScreen;
    //public Transform bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
    }

    
    void LateUpdate()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), turnSpeed * Time.deltaTime);

        exhaust.SetActive(true);
        if (Input.GetKey(KeyCode.Mouse1))
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, accSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Mouse0))
            vertical = 1;
        else
        {
            vertical = 0;
            exhaust.SetActive(false);
        }

        if (!hasLiftedOff && userHasControl && (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)))
            hasLiftedOff = true;

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    

    void FixedUpdate()
    {
        if (!userHasControl)
            return;

        Vector2 movement = new Vector2(vertical, 0);
        if (rb2d.velocity.y <= maxSpeed)
            rb2d.AddRelativeForce(movement * accSpeed);
        var main = stars.GetComponent<ParticleSystem>().main;
        main.simulationSpeed = rb2d.velocity.y * starSpeedModifier;
    }

    public void destroyShip()
    {
        Time.timeScale = 0;
        deadUIScreen.SetActive(true);
        Destroy(gameObject);
    }

    public void stopShip()
    {
        rb2d.velocity = Vector2.zero;
    }
}
