using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    private bool isTeaching;
    [SerializeField] private float teachingSpeed;
    [SerializeField] private float slidePercent = 0f;
    [SerializeField] private int slideNum = 0;
    [SerializeField] Text slideNumView;

    void OnTriggerEnter2D(Collider2D collision)
    {
        isTeaching = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isTeaching = false;
    }
    void FixedUpdate()
    {
        if (isTeaching && (slidePercent < 100f))
        {
            slidePercent += (0.01f * teachingSpeed) ;
        }
        slideNum = (int) slidePercent/10;
        slideNumView.text = slideNum.ToString() + "/10";
    }
}
