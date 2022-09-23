using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    private void Start()
    {
        manager = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        manager.RestartRunner(other.gameObject);
    }
}
