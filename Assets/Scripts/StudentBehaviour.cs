using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentBehaviour : MonoBehaviour
{
    public enum Behaviours
    {
        Normal,
        Sleeping,
        Talking,
        HandUp,
        
        Clear
    }


    private float learningSpeed;

    private float pointInLesson = 0f;
    private float lastPointInLesson = 0f;
    private Behaviours currentBehaviour;
    private Behaviours newBehaviour;
    private bool isHighlighted = false;

    [SerializeField] private SlideManager lesson;

    [SerializeField] private Text icon;
    private Text[] studentUI;
    [SerializeField] private int sleepChance = 3;
    [SerializeField] private int talkChance = 4;
    [SerializeField] private int handUpChance = 1;
    [SerializeField] private int inconsistency = 0;
    
    //how many behaviours you need to expect 1 behaviour per lesson.
    private float onePerLesson = 6f;
    
    private int totalChance;
    private float behaviourFreq;
    [SerializeField] private Behaviours[] behaviorChance;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private float behaviourPointInLesson = 0f;

    

    void Start()
    {
        studentUI = icon.GetComponentsInChildren<Text>();

        totalChance = sleepChance + talkChance + handUpChance;
        behaviourFreq = (onePerLesson / (float)totalChance) * 100f;

        minTime = Mathf.Clamp(1f, behaviourFreq - inconsistency, 150f);
        maxTime = Mathf.Clamp(1f, behaviourFreq + inconsistency, 150f);
        Debug.Log(minTime + "   " + maxTime);

        behaviorChance = new Behaviours[totalChance];

        for (int i = 0; i < sleepChance; i++)
        {
            behaviorChance[i] = Behaviours.Sleeping;
        }
        for (int i = sleepChance; i < (sleepChance + talkChance); i++)
        {
            behaviorChance[i] = Behaviours.Talking;
        }
        for (int i = (sleepChance + talkChance); i < totalChance; i++)
        {
            behaviorChance[i] = Behaviours.HandUp;
        }

        SetNormal();
    }

    void Update()
    {
        lastPointInLesson = pointInLesson;
        pointInLesson = lesson.SlidePercent;

        if (currentBehaviour == Behaviours.Normal)
        {
            if ((lastPointInLesson < behaviourPointInLesson) && (behaviourPointInLesson < pointInLesson))
            {
                newBehaviour = behaviorChance[Random.Range(0, totalChance)];
            }
        }

        Debug.Log(currentBehaviour);
        icon.text = currentBehaviour.ToString();
        if (isHighlighted)
        {
            icon.color = Color.red;
        }
        else
        {
            icon.color = Color.white;
        }
        //studentUI[1].enabled = isHighlighted;
    }

    public Behaviours GetBehaviour()
    {
        return currentBehaviour;
    }

    public void Interact()
    {
        if (currentBehaviour != Behaviours.Normal)
        {
            SetNormal();
        }
    }

    public void SetNormal()
    {
        learningSpeed = 1f;
        currentBehaviour = Behaviours.Normal;
        Debug.Log("set normal");
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
        currentBehaviour = Behaviours.Sleeping;
    }

    public void SetTalking()
    {
        learningSpeed = 0.25f;
        currentBehaviour = Behaviours.Talking;
    }

    public void SetHandUp()
    {
        learningSpeed = 0.75f;
        currentBehaviour = Behaviours.HandUp;
    }
    public void IconHighlight(bool highlight)
    {
        isHighlighted = highlight;
    }
}
