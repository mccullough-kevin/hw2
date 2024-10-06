// Author: Kevin McCullough
// Date: 10/5/2024
// Description: rotates camera on y axis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime, Space.World); // rotating on y
    }
}
