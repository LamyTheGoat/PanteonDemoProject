using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MoveRoute : MonoBehaviour
{
    [SerializeField] private Transform pointList;
    
    [SerializeField] private float speed;
    [SerializeField] private GameObject movingObject;

    private Transform moveFrom;
    private Transform moveTo;
    private int index = 0;
    private float fractionScalar = 0; 
    [SerializeField] private List<Transform> points;
    void Start()
    {
        points = new List<Transform>(pointList.GetComponentsInChildren<Transform>().Where(child => child.gameObject != this.gameObject));
        if (points.Count > 1)
        {
            moveFrom = points[0];
            moveTo = points[1];
            movingObject.transform.position = moveFrom.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count > 1)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        float distance = (moveTo.position - moveFrom.position).magnitude;
        fractionScalar += speed * Time.deltaTime;
        float frac = (fractionScalar / distance);
        movingObject.transform.position = Vector3.Lerp(moveFrom.position, moveTo.position, frac);
        if (frac >= 1)
        {
            fractionScalar = 0;
            index++;
            index = index % points.Count;
            moveFrom = points[index];
            if (index + 1 == points.Count)
            {
                moveTo = points[0];
            }
            else
            {
                moveTo = points[index + 1];
            }
        }
    }
}
