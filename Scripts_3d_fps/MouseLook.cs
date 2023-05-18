using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float sensitivity = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X"); // -1 to 1
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += mouseX * sensitivity;
        transform.localEulerAngles = newRotation;

    }
}
