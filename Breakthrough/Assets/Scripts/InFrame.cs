using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrame : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Vector3 viewPos;
    private Vector2 xMinMax, yMinMax;
    public bool ajustForRigidBody2D;
    public bool bounce;
    public bool ajustX, ajustY;
    // Start is called before the first frame update
    void Start()
    {
        var renderer = transform.GetComponent<SpriteRenderer>();
        objectWidth = renderer.bounds.size.x / 2;
        objectHeight = renderer.bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        viewPos = transform.position;

        xMinMax.x = screenBounds.x * -1 + objectWidth;
        xMinMax.y = screenBounds.x - objectWidth;
        if (ajustX)
            viewPos.x = Mathf.Clamp(viewPos.x, xMinMax.x, xMinMax.y);


        yMinMax.x = screenBounds.y - Camera.main.orthographicSize * 2 + objectHeight;
        yMinMax.y = screenBounds.y - objectHeight;
        if (ajustY)
            viewPos.y = Mathf.Clamp(viewPos.y, yMinMax.x, yMinMax.y);

        if (viewPos != transform.position)
        {
            if (ajustForRigidBody2D)
            {
                Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
                Vector2 vel = rb.velocity;
                if (viewPos.x != transform.position.x)
                    if (bounce)
                        vel.x *= -1;
                    else
                        vel.x = 0;

                if (viewPos.y != transform.position.y)
                    if (bounce)
                        vel.y *= -1;
                    else
                        vel.y = 0;

                rb.velocity = vel;
            }
            
            transform.position = viewPos;
        }
        
    }
}
