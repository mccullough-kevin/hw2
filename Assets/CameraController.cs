// Author: Kevin McCullough
// Date: 10/5/2024
// Description: sets camera position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position; // set camera position


    }
}
