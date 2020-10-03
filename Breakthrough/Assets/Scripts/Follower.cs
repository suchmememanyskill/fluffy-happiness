﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform toFollow;
    public bool followX, followY;
    public bool doNotGoBackOnY;
    public Vector2 offset;
    public Vector2 startFollow;
    public Vector2 moveMultiplier;

    private void Start()
    {
        if (moveMultiplier == Vector2.zero)
            moveMultiplier = new Vector2(1, 1);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (startFollow.x != 0 && startFollow.x > toFollow.position.x)
            return;

        if (startFollow.y != 0 && startFollow.y > toFollow.position.y)
            return;


        Vector3 temp = transform.position;

        if (followX)
            temp.x = (toFollow.position.x * moveMultiplier.x) + offset.x;
        
        if (followY)
            temp.y = (toFollow.position.y * moveMultiplier.y) + offset.y;

        if (doNotGoBackOnY && temp.y < transform.position.y)
            temp.y = transform.position.y;

        transform.position = temp;
    }
}
