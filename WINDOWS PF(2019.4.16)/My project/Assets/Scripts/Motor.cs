using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public Vector3 translation;
    public Vector3 rotation;

    private void Update()
    {
        transform.position += translation * Time.deltaTime;
        transform.Rotate(rotation * Time.deltaTime);
    }
}
