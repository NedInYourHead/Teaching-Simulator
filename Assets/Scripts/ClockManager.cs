using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private float lessonLengthSeconds;
    [SerializeField] private RectTransform clockHand;
    private float handRotation;
    private float currentSeconds;
    private float rotationAmount;
    private bool inLesson = true;
    [SerializeField] private StudentDataManager studentDataManager;

    void Start()
    {
        rotationAmount = 360 / lessonLengthSeconds;
    }

    void Update()
    {
        if (currentSeconds > lessonLengthSeconds && inLesson)
        {
            studentDataManager.EndLesson();
            //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            currentSeconds += Time.deltaTime;
            handRotation = -(currentSeconds * rotationAmount);
            clockHand.rotation = Quaternion.Euler(0f, 0f, handRotation);
        }
    }
    public float GetLessonLength()
    {
        return lessonLengthSeconds;
    }
}
