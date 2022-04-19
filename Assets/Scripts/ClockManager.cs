using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private float lessonLengthSeconds;
    [SerializeField] private RectTransform clockHand;
    private float handRotation;
    private float currentSeconds;
    private float rotationAmount;
    private bool inLesson = true;

    void Start()
    {
        rotationAmount = 360 / lessonLengthSeconds;
    }

    void Update()
    {
        if (currentSeconds > lessonLengthSeconds && inLesson)
        {
            Debug.Log("Lesson Over");
            inLesson = false;
            Time.timeScale = 0;
        }
        else
        {
            currentSeconds += Time.deltaTime;
            handRotation = -(currentSeconds * rotationAmount);
            clockHand.rotation = Quaternion.Euler(0f, 0f, handRotation);
        }
    }
}
