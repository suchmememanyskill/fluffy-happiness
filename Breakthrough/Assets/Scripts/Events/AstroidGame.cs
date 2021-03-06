﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidGame : MonoBehaviour
{
    private bool activated = false;
    private BoxCollider2D coll;
    public GameObject astroid;
    public GameObject mainCamera;
    private Vector2 screenBounds;
    private Vector2 objectBounds;
    public Vector2 spawnTimer;
    private float timer = 3;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        var renderer = astroid.GetComponent<SpriteRenderer>();
        objectBounds.x = renderer.bounds.size.x / 2;
        objectBounds.y = renderer.bounds.size.y / 2;
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            if (duration <= 0)
            {
                mainCamera.GetComponent<CameraTools>().startCamera();
                activated = false;
                return;
            }
            duration -= Time.fixedDeltaTime;

            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                return;
            }
                

            Vector3 pos = new Vector3(
                Random.Range(screenBounds.x * -1 + objectBounds.x, screenBounds.x - objectBounds.x),
                Random.Range(screenBounds.y - Camera.main.orthographicSize * 2 + objectBounds.y, screenBounds.y - objectBounds.y) + Camera.main.orthographicSize * 2,
                1);
            GameObject obj = Instantiate(astroid, pos, Quaternion.identity);
            timer = Random.Range(spawnTimer.x, spawnTimer.y);
        }
        else
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        activated = true;
        coll.enabled = false;
        mainCamera.GetComponent<CameraTools>().stopCamera();
    }
}
