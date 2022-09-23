using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class DonutObstacle : MonoBehaviour
{
    [SerializeField] private bool isEnabled = false;
    [SerializeField] private bool riskEnabled = false;
    [SerializeField] private float minInterval = 2;
    [SerializeField] private float maxInterval = 5;
    [SerializeField] private Vector3 enabledPosition = new Vector3(0,0,0);
    [SerializeField] private Vector3 disabledPosition = new Vector3(0,0,0);
    [SerializeField] private float movementTime = 0.6f;
    [SerializeField] private float RiskCalculationTime = 0.2f;
    [SerializeField]private GameObject movingStick;
    [SerializeField]private GameObject highRiskArea;

    private float counter = 0;
    private float startTime = 0;
    private float riskStartTime = 0;
    private Vector3 destination;
    private Vector3 oldPosition;
    private Vector3 riskDestination;
    private Vector3 riskOldPosition;
    private bool riskChangeable = true;



    // Start is called before the first frame update
    void Start()
    {
        destination = disabledPosition;
        riskDestination = disabledPosition;
        movingStick.transform.localPosition = destination;
        if(highRiskArea) {highRiskArea.transform.localPosition = riskDestination;}
        SetNewInterval();
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        
        //IF RISK AREA EXIST MAKE RISK CALCULATIONS JUST BEFORE THE ACTUAL MOVEMENT
        if(highRiskArea){if (counter <= RiskCalculationTime)
        {
            if (riskChangeable)
            {
                riskChangeable = false;
                riskOldPosition = highRiskArea.transform.localPosition;
                riskStartTime = Time.time;
                riskEnabled = !riskEnabled;
                if (riskEnabled)
                {
                    riskDestination = enabledPosition;
                }
                else
                {
                    riskDestination = disabledPosition;
                }
            }

        }}
        
        
        //SET THE DESTINATION AFTER COUNTER REACHES ZERO
        if (counter <= 0)
        {
            oldPosition = movingStick.transform.localPosition;
            startTime = Time.time;
            isEnabled = !isEnabled;
            if (isEnabled)
            {
                destination = enabledPosition;
            }
            else
            {
                destination = disabledPosition;
            }
            
            SetNewInterval();
        }
        
        MoveStick();
        
    }

    void SetNewInterval()
    {
        riskChangeable = true;
        counter = Random.Range(minInterval, maxInterval);
    }

    void MoveStick()
    {
        float fracStick = (Time.time - startTime) / movementTime;
        movingStick.transform.localPosition = Vector3.Lerp(oldPosition, destination, fracStick);
        
        if(highRiskArea == null) return;
        float fracRisk = (Time.time - riskStartTime) / movementTime;
        highRiskArea.transform.localPosition = Vector3.Lerp(riskOldPosition, riskDestination, fracRisk);
    }
    

    
    
}
