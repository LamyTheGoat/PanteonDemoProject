using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool lookForPlayer = true;
    public UnityEvent triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (lookForPlayer && other.gameObject.name != "Player")
        {
            return;
        }
        
        triggerEvent.Invoke();

    }
}
