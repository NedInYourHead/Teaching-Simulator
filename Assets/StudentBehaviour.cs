using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentBehaviour : MonoBehaviour
{
    private float learningSpeed;
    private string currentBehaviour;

    public void SetNormal()
    {
        learningSpeed = 100f;
        currentBehaviour = "normal";
    }
    
    public void SetSleeping()
    {
        learningSpeed = 0f;
        currentBehaviour = "sleeping";
    }

    public void SetTalking()
    {
        learningSpeed = 25f;
        currentBehaviour = "talking";
    }

    public void SetHandUp()
    {
        learningSpeed = 75f;
        currentBehaviour = "handUp";
    }
}
