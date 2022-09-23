using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovingObject selfMOScript;
    
    // Start is called before the first frame update
    void Start()
    {
        selfMOScript = GetComponent<MovingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        selfMOScript.SetHorizontalInput(Input.GetAxisRaw("Horizontal"));
    }
}
