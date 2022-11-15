using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Train : MonoBehaviour
{
    private float percent = 0;
    private float step = 0;
    private float acceleration = 10f;
   
    private bool canStart = false;
    private Transform startPosition;
    private Transform endPosition;

    private bool ReadyToGo = false;

    int stage = 0;
    
    void Update()
    {
        if(canStart) TrainMove();
    }

    public void Departure(Transform start, Transform end)
    {
        startPosition = start;
        endPosition = end;
        canStart = true;
    }

    private void TrainMove()
    {
        switch(stage)
        {
            case 0:
                percent = 0;
                stage = 1;
                break;
            case 1:
                if (percent > 0.07f) stage = 2;
                else step += acceleration * Time.deltaTime / 15000;
                break;
            case 2:
                if(percent > 0.93f) stage = 3;
                break;
            case 3:
                step -= acceleration * Time.deltaTime / 25000;
                if (step < 0)
                {
                    step = 0;
                    percent = 1;
                    stage = 0;
                    canStart = false;
                }
                break;
            default: break;
        }
        percent += step * Time.deltaTime * 1000;
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percent);  
    }

    public void OnMouseDown()
    {
        ReadyToGo = true;
    }
}
