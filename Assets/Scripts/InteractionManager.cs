using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    NewStudentBehaviour behaviour;
    BoxCollider2D triggerBox;
    private SlideManager slideManager;
    private bool isTouching;
    public bool showBar;

    public string slideKey = "a";
    private bool isExplaining;
    private float barMax;
    private float explainingSpeed;
    [SerializeField] private float barPercent;
    public static float timer;

    private void Awake()
    {
        slideManager = GameObject.Find("SlideBox").GetComponent<SlideManager>();
        barMax = (100f/slideManager.slideTotal);
        explainingSpeed = slideManager.teachingSpeed;
        barPercent = 0;
        behaviour = GetComponentInParent<NewStudentBehaviour>();
        triggerBox = GetComponent<BoxCollider2D>();
        isTouching = false;
        timer = 0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }

    void Update()
    {
        if (DialogueManager.instance.dialogueIsPlaying)
        {
            isTouching = false;
            if (DialogueManager.instance.choiceNum == 0 && Input.GetKeyDown("space"))
            {
                DialogueManager.instance.ContinueStory();
            }
            if (DialogueManager.instance.choiceNum > 0)
            {
                if (showBar)
                {
                    //show bar
                    if (Input.GetKey(slideKey))
                    {
                        isExplaining = true;
                    }
                    else if (isExplaining && (!Input.GetKey(slideKey)))
                    {
                        isExplaining = false;
                    }
                }
                else if (Input.GetKeyDown("1"))
                {
                    DialogueManager.instance.MakeChoice(0);
                }
            }
            if (DialogueManager.instance.choiceNum > 1 && Input.GetKeyDown("2"))
            {
                DialogueManager.instance.MakeChoice(1);
            }
        }
        else if (isTouching && Input.GetKeyDown("space"))
        {
            behaviour.Interact();
        }
        behaviour.IconHighlight(isTouching);
    }
    void FixedUpdate()
    {
        if (isExplaining)
        {
            barPercent += (0.01f * explainingSpeed);
        }
    }
}
