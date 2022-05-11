using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterLessonIcon : MonoBehaviour
{
    private Text[] studentUI;
    private string studentNotif = "";
    private int studentNum = 10;
    public int StudentNum
    {
        get {return studentNum;}
        set
        {
            if (studentNum == 10)
            {
                studentNum = value;
            }
        }
    }

    void Awake()
    {
        studentUI = GetComponentsInChildren<Text>();
    }
    void Start()
    {
        if (RetainedData.hookDiscovered[StudentNum] == true)
        {
            studentNotif = "!";
            studentUI[0].color = Color.cyan;
        }
        studentUI[0].text = StudentNum.ToString() + studentNotif;
    }
}
