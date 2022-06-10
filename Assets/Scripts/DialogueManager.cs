using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private bool hookDiscovered = false;

    private Story currentStory;

    private NewStudentBehaviour currentStudent;

    public bool dialogueIsPlaying {get; private set;}
    
    public int choiceNum {get; private set;}

    public static DialogueManager instance {get; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
            choicesText[i].text = "";
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, NewStudentBehaviour thisStudent)
    {
        currentStory = new Story(inkJSON.text);
        //set all the variables here
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        currentStudent = thisStudent;
        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        choiceNum = currentChoices.Count;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("too many choices in story");
        }

        for (int i = 0; i < choices.Length; i++)
        {
            Debug.Log((i < (currentChoices.Count)).ToString() + i.ToString());
            if (i < (currentChoices.Count))
            {
                choices[i].gameObject.SetActive(true);
                choicesText[i].text = currentChoices[i].text;
            }
            else
            {
                choices[i].gameObject.SetActive(false);
            }
        }
    }

    public void MakeChoice(int decision)
    {
        currentStory.ChooseChoiceIndex(decision);
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        //dialogueText.text = "";
        currentStudent.FinishDialogue(hookDiscovered);
        currentStudent = null;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
}
