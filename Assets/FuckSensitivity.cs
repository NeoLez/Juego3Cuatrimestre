using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckSensitivity : MonoBehaviour
{

    // Update is called once per frame
    void Update() {
        GetComponent<CameraController>().sensitivity = 0.15f;
    }
}
