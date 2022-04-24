using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentBehaviour : MonoBehaviour
{
    private float learningSpeed;
    private float pointInLesson = 0f;
    private float lastPointInLesson = 0f;
    private string currentBehaviour;
    private string newBehaviour;
    private bool isHighlighted = false;

    [SerializeField] private SlideManager lesson;

    [SerializeField] private Text icon;
    [SerializeField] private int sleepChance = 3;
    [SerializeField] private int talkChance = 4;
    [SerializeField] private int handUpChance = 1;
    [SerializeField] private int inconsistency = 0;
    
    //how many behaviours you need to expect 1 behaviour per lesson.
    private float timesInLesson = 9f;
    
    private int totalChance;
    private float behaviourFreq;
    [SerializeField] private string[] behaviorChance;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private float behaviourPointInLesson = 0f;

    void Start()
    {
        totalChance = sleepChance + talkChance + handUpChance;
        behaviourFreq = (timesInLesson / (float)totalChance) * 100f;

        minTime = Mathf.Clamp(1f, behaviourFreq - inconsistency, 150f);
        maxTime = Mathf.Clamp(1f, behaviourFreq + inconsistency, 150f);
        Debug.Log(minTime + "   " + maxTime);

        behaviorChance = new string[totalChance];

        for (int i = 0; i < sleepChance; i++)
        {
            behaviorChance[i] = "sleeping";
        }
        for (int i = sleepChance; i < (sleepChance + talkChance); i++)
        {
            behaviorChance[i] = "talking";
        }
        for (int i = (sleepChance + talkChance); i < totalChance; i++)
        {
            behaviorChance[i] = "handUp";
        }

        SetNormal();
    }

    void Update()
    {
        lastPointInLesson = pointInLesson;
        pointInLesson = lesson.GetSlidePercent();

        if (currentBehaviour == "normal")
        {
            if ((lastPointInLesson < behaviourPointInLesson) && (behaviourPointInLesson < pointInLesson))
            {
                newBehaviour = behaviorChance[Random.Range(0, totalChance)];
            }
        }

        if (newBehaviour == "sleeping")
        {
            SetSleeping();
        }
        else if (newBehaviour == "talking")
        {
            SetTalking();
        }
        else if (newBehaviour == "handUp")
        {
            SetHandUp();
        }
        newBehaviour = "";
        icon.text = currentBehaviour;
        if (isHighlighted)
        {
            icon.color = Color.red;
        }
        else
        {
            icon.color = Color.white;
        }
    }

    public string GetBehaviour()
    {
        return currentBehaviour;
    }

    public void SetNormal()
    {
        learningSpeed = 1f;
        currentBehaviour = "normal";
        behaviourPointInLesson = 100f;
        while (behaviourPointInLesson == 100f)
        {
            behaviourPointInLesson = pointInLesson + Random.Range(minTime, maxTime);
        }
        Debug.Log(behaviourPointInLesson);
    }
    
    public void SetSleeping()
    {
        learningSpeed = 0f;
        currentBehaviour = "sleeping";
    }

    public void SetTalking()
    {
        learningSpeed = 0.25f;
        currentBehaviour = "talking";
    }

    public void SetHandUp()
    {
        learningSpeed = 0.75f;
        currentBehaviour = "handUp";
    }
    public void IconHighlight(bool highlight)
    {
        isHighlighted = highlight;
    }
}
