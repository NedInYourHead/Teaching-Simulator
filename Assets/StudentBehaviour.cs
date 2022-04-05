using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentBehaviour : MonoBehaviour
{
    private float learningSpeed;
    private string currentBehaviour;

    [SerializeField] private Text icon;

    private float minTime = 20;
    private float maxTime = 60;
    private float timeUntilAbnormal = 0f;

    void Start()
    {
        SetNormal();
    }

    void FixedUpdate()
    {
        if (currentBehaviour == "normal")
        {
            if (timeUntilAbnormal <= 0)
            {
                SetSleeping();
                Debug.Log("set rand behaviour");
            }
            timeUntilAbnormal -= Time.deltaTime;
        }
        icon.text = currentBehaviour;
    }

    public string GetBehaviour()
    {
        return currentBehaviour;
    }

    public void SetNormal()
    {
        learningSpeed = 100f;
        currentBehaviour = "normal";
        timeUntilAbnormal = Random.Range(minTime, maxTime);
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
