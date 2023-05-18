using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    private float sensitivity = 2f;
    private float rotationY = 0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     rotationY -= Input.GetAxis ("Mouse Y") * sensitivity;
     rotationY = Mathf.Clamp (rotationY, -60f, 60f);
     Vector3 newRotation = transform.localEulerAngles;
     newRotation.x = rotationY;
     transform.localEulerAngles = newRotation;
    }
}
