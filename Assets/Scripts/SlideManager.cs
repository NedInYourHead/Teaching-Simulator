using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    private bool isInRadius;
    [SerializeField] private float teachingSpeed;
    [SerializeField] private float slidePercent = 0f;
    [SerializeField] private int slideNum = 0;
    [SerializeField] private int slideTotal = 10;
    private int nextSlide = 0;

    [SerializeField] private Text slideNumView;
    [SerializeField] private Slider percentSlider;
    [SerializeField] private Slider slideSlider;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private bool hideProgressBar;

    [SerializeField] private Text keyView;

    private string[] keyList = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
    [SerializeField] private string[] slideKeys;
    private string currentKey;
    private bool keyIsPressed = false;
    

    private void Start()
    {
        percentSlider.maxValue = slideTotal;

        slideKeys = new string[slideTotal];

        for (int i = 0; i < slideTotal; i++)
        {
            slideKeys[i] = keyList[Random.Range(0, keyList.Length)];
        }
        Debug.Log(slideKeys[0]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRadius = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRadius = false;
    }

    private void Update()
    {
        slideNum = (int)(slidePercent / (100f / slideTotal));

        if (slideNum == nextSlide)
        {
            currentKey = slideKeys[slideNum];
            nextSlide += 1;
            percentSlider.value = nextSlide;
        }

        keyIsPressed = Input.GetKey(currentKey);

        //display all the things
        slideNumView.text = slideNum.ToString() + "/" + slideTotal + " Slides";
        slideSlider.value = slidePercent;
        keyView.text = currentKey;
    }

    private void FixedUpdate()
    {
        //shows/hides progressbar
        if (hideProgressBar)
        {
            progressBar.SetActive(isInRadius);
        }
        //increment the slide percentage
        if (isInRadius && (slideNum < slideTotal) && keyIsPressed)
        {
            slidePercent += (0.01f * teachingSpeed);
        }
    }
}
