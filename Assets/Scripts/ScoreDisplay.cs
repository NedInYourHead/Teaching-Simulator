using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text[] scores;
    private float averageScore;
    private int highest = 0;
    private int lowest = 0;
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            averageScore += RetainedData.studentCurriculum[i];
            if (RetainedData.studentCurriculum[i] > RetainedData.studentCurriculum[highest])
            {
                highest = i;
            }
            if (RetainedData.studentCurriculum[i] < RetainedData.studentCurriculum[lowest])
            {
                lowest = i;
            }
        }
        averageScore = averageScore/9;

        scores = GetComponentsInChildren<Text>();
        scores[1].text = averageScore.ToString() + "%";
        scores[2].text = "Student " + highest;
        scores[3].text = RetainedData.studentCurriculum[highest].ToString() + "%";
        scores[4].text = "Student " + lowest.ToString();
        scores[5].text = RetainedData.studentCurriculum[lowest].ToString() + "%";
    }

    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            RetainedData.Reset();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
