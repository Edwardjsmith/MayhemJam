﻿using UnityEngine;

public class FPSCam : MonoBehaviour
{
    float sensitivity = 100.0f;
    float xAxisClamp = 0;
    Transform playerBody;
    bool bodyAttached = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Use this for initialization
    public void Start()
    {
        if (transform.parent != null)
        {
            playerBody = transform.parent;
            bodyAttached = true;
        }
    }

    private void Update()
    {
        camRotation();
    }

    void camRotation()
    {
        float mouseX = -Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxis(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxis(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);

        if (bodyAttached)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else if(!bodyAttached)
        {
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void ClampXAxis(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }


}
