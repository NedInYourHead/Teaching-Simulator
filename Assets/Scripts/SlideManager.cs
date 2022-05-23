using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    private bool isInRadius;
    [SerializeField] private float teachingSpeed;
    [SerializeField] private float slidePercent = 0f;
    
    //set: slidePercent cannot be set above 100 or below 0
    public float SlidePercent 
    {
        get
        {
            return slidePercent;
        }
        private set
        {
            slidePercent = Mathf.Clamp(0f, value, 100f);
        }
    }


    private int lastPercent;
    private float difference;
    [SerializeField] private StudentDataManager studentManager;
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
    [SerializeField] private bool inputIncreasesSlides = false;

    //set the slides slider to the total num of slides, creates the list of keys to be used in the slides, and adds lesson complete to the end.
    private void Start()
    {
        slideSlider.maxValue = slideTotal;

        slideKeys = new string[slideTotal+1];

        for (int i = 0; i < slideTotal; i++)
        {
            slideKeys[i] = keyList[Random.Range(0, keyList.Length)];
        }
        slideKeys[slideTotal] = "Lesson Complete";
    }

    //if in radius, highlight the icon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRadius = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRadius = false;
    }

    //every frame, slideNumber = slidepercent/(total percent/total num of slides), on the frame the slide ends, call the end of
    //slide method from studentManager, set the next slide key, and add 1 to nextslide, which is displayed. While slideNum is
    //smaller than the total number of slides, advance them if the correct button is held.
    private void Update()
    {
        slideNum = (int)(SlidePercent / (100f / slideTotal));

        if (slideNum == nextSlide)
        {
            studentManager.EndOfSlide(SlidePercent);
            currentKey = slideKeys[slideNum];
            nextSlide += 1;
            slideSlider.value = nextSlide;
        }

        if (slideNum < slideTotal)
        {
            if (!inputIncreasesSlides)
            {
                keyIsPressed = Input.GetKey(currentKey);
            }
            else
            {
                keyIsPressed = (Input.GetAxis("Fire1") > 0);
            }
        }
        else
        {
            SlidePercent = 100f;
            keyIsPressed = false;
        }

        //display all the things
        slideNumView.text = slideNum.ToString() + "/" + slideTotal + " Slides";
        percentSlider.value = SlidePercent;
        keyView.text = currentKey.ToUpper();
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
            SlidePercent += (0.01f * teachingSpeed);

            difference = (SlidePercent - lastPercent);
            if (difference > 1f)
            {
                for (int i = 0; i < (int)difference; i++)
                {
                    studentManager.OnePercent();
                }
                lastPercent = (int)SlidePercent;
            }
        }
    }
}
