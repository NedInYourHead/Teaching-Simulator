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
    [SerializeField] private Slider percentSlider;
    [SerializeField] private Slider slideSlider;

    private void Start()
    {
        percentSlider.maxValue = slideTotal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTeaching = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTeaching = false;
    }
    
    private void FixedUpdate()
    {
        //increment the slide percentage
        if (isTeaching && (slideNum < slideTotal))
        {
            slidePercent += (0.01f * teachingSpeed) ;
        }
        slideNum = (int) (slidePercent/(100f/slideTotal));
    }

    private void Update()
    {
        //display the slide progress
        slideNumView.text = slideNum.ToString() + "/" + slideTotal + " Slides";
        slideSlider.value = slidePercent;
        percentSlider.value = slideNum+1;
    }
}
