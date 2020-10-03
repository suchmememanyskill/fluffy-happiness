using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipliedFollower : MonoBehaviour
{
    public Transform toFollow;
    public Vector2 moveMultiplier;
    private Vector2 startVector;
    private Vector2 startVectorSelf;

    private void Start()
    {
        if (moveMultiplier == Vector2.zero)
            moveMultiplier = new Vector2(1, 1);

        startVector = toFollow.position;
        startVectorSelf = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 temp = startVectorSelf;

        temp += ((Vector2)toFollow.position - startVector) * moveMultiplier;

        transform.position = new Vector3(temp.x, temp.y, transform.position.z);
    }
}
