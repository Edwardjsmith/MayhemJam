using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 10;
    public Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.C))
        {

        }
    }
}
