using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    private bool isTeaching;
    [SerializeField] private float teachingSpeed;
    [SerializeField] private float slidePercent = 0f;
    [SerializeField] private float slideNum = 0;
    [SerializeField] private int slideTotal = 10;
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
        if (isTeaching && (slideNum < slideTotal))
        {
            slidePercent += (0.01f * teachingSpeed) ;
        }
        slideNum = (int) (slidePercent/(100f/slideTotal));
        slideNumView.text = slideNum.ToString() + "/" + slideTotal;
    }
}
