using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 rotationAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAmount.x * Time.deltaTime, rotationAmount.y * Time.deltaTime, rotationAmount.z * Time.deltaTime, Space.Self);
    }
}
