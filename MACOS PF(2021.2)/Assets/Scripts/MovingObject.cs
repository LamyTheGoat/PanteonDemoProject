using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float objectSpeedForward = 3f; 
    public float objectSpeedSideways = 5f;
    
    private float horizontalInput = 0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rb)
        {
            Vector3 calculatedVelociy = (transform.right * horizontalInput * objectSpeedSideways) + (transform.forward * objectSpeedForward);
            calculatedVelociy.y = rb.velocity.y;
            rb.velocity = calculatedVelociy;
        }
    }

    public void SetHorizontalInput(float HorizontalInput)
    {
        horizontalInput = HorizontalInput;
    }
}
