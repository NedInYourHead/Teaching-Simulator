using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStudentBehaviour : MonoBehaviour
{

    [Header("Ink JSON")]
    public TextAsset inkJSON;
    [SerializeField] private Text icon;
    private Text[] studentUI;
    private bool isHighlighted = false;

    private StudentDataManager studentManager;

    //StudentNum starts at 10 and can only be changed if it equals 10, meaning it can be changed
    //once and otherwise is read-only
    private string studentName;
    private int studentNum = 10;
    public int StudentNum
    {
        get {return studentNum;}
        set
        {
            if (studentNum == 10)
            {
                //studentName = studentManager.studentNames[value];
                studentNum = value;
            }
        }
    }

    //Behaviours are the 4 states a student can display, the names are displayed as strings
    //for debugging, but will be represented by icons or animations in the full game
    public enum Behaviours
    {
        none,
        normal,
        sleeping,
        talking,
        handUp
    }

    private Behaviours currentBehaviour = Behaviours.normal;
    private float learningPoints;
    private float learningSpeed;
    [SerializeField] private float maxLearningSpeed = 0.9f;

    //Prevents learningPoints from going above 100% in a lesson, or below 0%
    public float LearningPoints
    {
        get {return learningPoints;}
        set
        {
            learningPoints = Mathf.Clamp(0f, value, 100f);
        }
    }

    //The first switch statement determines what each behaviour does when it ends, the second
    //sets up each behaviour by setting the learningSpeed and doing other things.
    public Behaviours CurrentBehaviour
    {
        get
        {return currentBehaviour;}
        private set
        {
            switch (currentBehaviour)
            {
                case Behaviours.normal:
                    //end normal
                    break;

                case Behaviours.sleeping:
                    //end sleeping
                    break;

                case Behaviours.talking:
                    //end talking
                    studentManager.IAmTalking(StudentNum, false);
                    break;

                case Behaviours.handUp:
                    //end handUp
                    break;
            }
            switch (value)
            {
                case Behaviours.normal:
                    learningSpeed = 1f;
                    break;

                case Behaviours.sleeping:
                    learningSpeed = 0f;
                    break;

                case Behaviours.talking:
                    learningSpeed = 0.25f;
                    studentManager.IAmTalking(StudentNum, true);
                    break;

                case Behaviours.handUp:
                    learningSpeed = 0.75f;
                    handUpTimer = handUpPatience;
                    break;
            }
            currentBehaviour = value;
        }
    }


    [SerializeField] private Behaviours hookBehaviour = Behaviours.none;
    [SerializeField] private bool hookDiscovered;
    public bool HookDiscovered
    {
        get {return hookDiscovered;}
        set
        {
            if (!hookDiscovered)
            {
                hookDiscovered = value;
            }
        }
    }

    [SerializeField] private int sleepChance = 0;
    [SerializeField] private int talkChance = 0;
    private int handUpChance = 0;
    [SerializeField] private float handUpConfidence = 0.4f;
    [SerializeField] private float handUpThreshold = 0.075f;
    [SerializeField] private float handUpPatience = 2.5f;
    [SerializeField] private float handDownDiscouragement = 0.8f;
    [SerializeField] private int handDownSleepChance = 50;
    [SerializeField] private float AnswerBonus = 10f;
    private float handUpTimer = 0f;
    [SerializeField] private bool hasHandBeenUp = false;

    //these set/get each chance and refreshes chance list for the specific chance
    public int SleepChance
    {
        get {return sleepChance;}
        set
        {
            sleepChance = value;
            RefreshChance(Behaviours.sleeping, sleepChance);
        }
    }

    public int TalkChance
    {
        get {return talkChance;}
        set
        {
            talkChance = value;
            RefreshChance(Behaviours.talking, talkChance);
        }
    }

    public int HandUpChance
    {
        get {return handUpChance;}
        set
        {
            handUpChance = value;
            RefreshChance(Behaviours.handUp, handUpChance);
        }
    }

    [SerializeField] private float chancePerBehaviour = 1f;
    private int totalBehaviours;
    private float behaviourChance;
    [SerializeField] private List<Behaviours> chance = new List<Behaviours>();



//                                 /\
//                                /__\
//       VARIABLES + PROPERTIES    ||
//                                 ||


    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx




//                                 ||
//              METHODS            ||
//                                \--/
//                                 \/




    //private int methodCalled = 0;

    //gets info displays (debugging and ui), resets LearningPoints, sets currentBehaviour to normal,
    //Sets learningSpeed to 1, sets totalbehaviours to the total of all chances, refreshes chance,
    //then gets studentDataManager
    void Start()
    {
        studentUI = icon.GetComponentsInChildren<Text>();

        LearningPoints = 0f;

        CurrentBehaviour = Behaviours.normal;
        learningSpeed = 1f;
        totalBehaviours = SleepChance + TalkChance + HandUpChance;
        RefreshAll();

        studentManager = GetComponentInParent<StudentDataManager>();

        hasHandBeenUp = false;

        SetUpDisplay(studentName);
    }

    //initializes display once SDM has set everything up
    public void SetUpDisplay(string name)
    {
        studentUI[4].text = name;
    }

    //calls RefreshChance for each behaviour
    public void RefreshAll()
    {
        RefreshChance(Behaviours.sleeping, SleepChance);
        RefreshChance(Behaviours.talking, TalkChance);
        RefreshChance(Behaviours.handUp, HandUpChance);
    }
    //Removes all instances of a specific behaviour, and adds new ones equal to behaviourAmount integer.
    private void RefreshChance(Behaviours whichChance, int behaviourAmount)
    {
        chance.RemoveAll(x =>  x == whichChance);

        for (int i = 0; i < behaviourAmount; i++)
        {
            chance.Add(whichChance);
        }

        totalBehaviours = SleepChance + TalkChance + HandUpChance;
        behaviourChance = chancePerBehaviour * (float)totalBehaviours;
    }
    
    //Called from StudentDataManager, which is called by SlideManager, each time the
    //slide percentage goes up by 1.
    //adds learning points and chooses whether to set a behaviour based on chance values
    public void EachPercent()
    {
        if ((handUpPatience > 0) && (CurrentBehaviour == Behaviours.handUp))
        {
            handUpPatience -= 1f;
        }
        else if ((handUpPatience <= 0) && (CurrentBehaviour == Behaviours.handUp))
        {
            RunOutOfPatience();
        }
        if (LearningPoints <= 100f)
        {
            LearningPoints += (1f * maxLearningSpeed) * learningSpeed;
        }
        if (CurrentBehaviour == Behaviours.normal)
        {
            if (Random.Range(0, 10000) < behaviourChance)
            {
                CurrentBehaviour = chance[Random.Range(0, chance.Count)];
            }
        }
    }

    //is called whenever the slidemanager reaches the next slide, from StudentDataManager, called
    //by SlideManager.
    //if the student is far enough behind, has a chance to put their hand up.
    public void NewSlide(float percent)
    {
        if ((0 < percent) && (percent > (LearningPoints + handUpThreshold)) && (percent < 100f))
        {
            if ((Random.Range(0f, 1f) <= handUpConfidence) && (CurrentBehaviour == Behaviours.normal))
            {
                CurrentBehaviour = Behaviours.handUp;
            }
        }
    }

    //runs if the student has their hand up for too long
    public void RunOutOfPatience()
    {
        CurrentBehaviour = Behaviours.normal;
        handUpConfidence *= handDownDiscouragement;
        if (!hasHandBeenUp)
        {
            hasHandBeenUp = true;
            SleepChance += handDownSleepChance;
        }
        
    }

    //every frame, displays values in ui
    void Update()
    {
        icon.text = CurrentBehaviour.ToString();


        if (isHighlighted)
        {
            studentUI[4].color = Color.red;
        }
        else
        {
            studentUI[4].color = Color.white;
        }
        //studentUI[1].enabled = isHighlighted;
        studentUI[2].text = LearningPoints.ToString();
        studentUI[3].text = TalkChance.ToString();
    }

    // is called by the interaction manager for this student whenever it is interacted with
    public void Interact()
    {
        DialogueManager.instance.EnterDialogueMode(inkJSON, this);
    }

    public void FinishDialogue(bool hook)
    {
        if (CurrentBehaviour == Behaviours.handUp)
        {
            HandUpAnswered();
        }
        hookDiscovered = hook;
        CurrentBehaviour = Behaviours.normal;
    }

    public void HandUpAnswered()
    {
        LearningPoints = LearningPoints + AnswerBonus;
        studentManager.AnsweredMyQuestion();
    }
    
    //makes the student ui indicate when in range of interaction 
    public void IconHighlight(bool highlight)
    {
        isHighlighted = highlight;
    }
}
