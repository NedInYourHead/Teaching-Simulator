using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterLessonManager : MonoBehaviour
{
    private AfterLessonIcon[] students;

    void Awake()
    {
        students = GetComponentsInChildren<AfterLessonIcon>();
        for (int i = 0; i < 9; i++)
        {
            students[i].StudentNum = i;
        }
    }
}
