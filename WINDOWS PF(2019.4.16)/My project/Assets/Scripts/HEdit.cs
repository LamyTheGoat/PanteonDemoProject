using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEdit : MonoBehaviour
{
    private Dictionary<GameObject, Transform> childParentDict;
    [SerializeField] private bool changeHierarchy = true;
    [SerializeField] private bool changePositioning = true;
    void Start()
    {
        childParentDict = new Dictionary<GameObject, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!changeHierarchy) return;
        if (other.gameObject.tag == "MOB")
        {
            childParentDict.Add(other.gameObject, other.gameObject.transform.parent);
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionStay(Collision other)
    {   if (!changePositioning) return;
        if (other.gameObject.tag == "MOB")
            other.gameObject.transform.rotation =
                (Quaternion.LookRotation(other.gameObject.transform.forward, other.GetContact(0).normal * -1));
    }

    private void OnTriggerExit(Collider other)
    {   if (!changeHierarchy) return;
        if (other.gameObject.tag == "MOB")
        {
            other.gameObject.transform.parent = childParentDict[other.gameObject];
            childParentDict.Remove(other.gameObject);
        }

    }
}
