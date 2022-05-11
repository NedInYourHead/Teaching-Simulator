using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDataManager : MonoBehaviour
{
    public static StudentDataManager Instance;

    /*
    Students:
    6 7 8
    3 4 5
    0 1 2
    */

    [SerializeField] private NewStudentBehaviour[] students;
    [SerializeField] private int generalSleepChanceMax = 40;
    [SerializeField] private int generalTalkChanceMax = 20;
    [SerializeField] private int generalHandUpChanceMax = 20;
    [SerializeField] private int conversationContagion = 100;
    [SerializeField] private float classQuestionBonus = 5f;
    private int talkModifier = 0;

    void Awake()
    {
        students = GetComponentsInChildren<NewStudentBehaviour>();

        for (int i = 0; i < students.Length; i++)
        {
            students[i].SleepChance += Random.Range(0, generalSleepChanceMax);
            students[i].TalkChance += Random.Range(0, generalTalkChanceMax);
            students[i].HandUpChance += Random.Range(0, generalHandUpChanceMax);
            students[i].StudentNum = i;
            students[i].HookDiscovered = RetainedData.hookDiscovered[i];
        }
    }

    public void OnePercent()
    {
        for (int i = 0; i < students.Length; i++)
        {
            students[i].EachPercent();
        }
    }

    public void EndOfSlide(float slidePercent)
    {
        for (int i = 0; i < students.Length; i++)
        {
            students[i].NewSlide(slidePercent);
        }
    }
    
    public void IAmTalking(int studentNum, bool isTalking)
    {
        if (isTalking)
        {
            talkModifier = conversationContagion;
        }
        else
        {
            talkModifier = -conversationContagion;
        }
        if (!(studentNum > 5))
        {
            students[studentNum + 3].TalkChance += talkModifier;  
        }
        if (!(studentNum < 3))
        {
            students[studentNum - 3].TalkChance += talkModifier;
        }
        if ((studentNum % 3) != 0)
        {
            students[studentNum - 1].TalkChance += talkModifier;
        }
        if ((studentNum % 3) != 2)
        {
            students[studentNum + 1].TalkChance += talkModifier;
        }
    }
    
    public void AnsweredMyQuestion()
    {
        for (int i = 0; i < students.Length; i++)
        {
            if((students[i].CurrentBehaviour == NewStudentBehaviour.Behaviours.normal)||(students[i].CurrentBehaviour == NewStudentBehaviour.Behaviours.handUp))
            {
                students[i].LearningPoints += classQuestionBonus;
            }
        }
    }

    public void EndLesson()
    {
        for (int i = 0; i < students.Length; i++)
        {
            RetainedData.studentCurriculum[i] += students[i].LearningPoints;
            RetainedData.hookDiscovered[i] = students[i].HookDiscovered;
        }
    }
}
