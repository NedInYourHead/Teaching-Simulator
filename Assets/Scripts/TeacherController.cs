using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    private string movePriority;

    private Animator animator;
    private string currentState;

    private float vertical;
    [SerializeField] private float speed = 0.2f;

    private float horizontal;

    private SpriteRenderer teacherRenderer;
    private SpriteRenderer shadowRenderer;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        teacherRenderer = GetComponentInChildren<SpriteRenderer>();
        shadowRenderer = GetComponent<SpriteRenderer>();
    }

    void PlayAnim(string newState)
    {
        if (currentState == newState)
        {
            animator.Play(newState);
        }
        currentState = newState;
    }

    void FixedUpdate()
    {

        // the reason this part is so complex is because it only lets you move in x OR y and prioritises the direction you're NOT holding.
        // probably need to clean it up and make it more elegant
        horizontal = Input.GetAxis("Horizontal");

        vertical = Input.GetAxis("Vertical");

        if (horizontal == 0f)
        {
            movePriority = "x";
        }
        if (vertical == 0f)
        {
            movePriority = "y";
        }

        if (movePriority == "x" || vertical == 0f)
        {
            transform.position = transform.position + (transform.right * horizontal * speed);
            if (horizontal > 0)
            {
                PlayAnim("RightWalk");
            }
            else if (horizontal < 0)
            {
                PlayAnim("LeftWalk");
            }
        }
        if (movePriority == "y" || horizontal == 0f)
        {
            transform.position = transform.position + (transform.up * vertical * speed);
            if (vertical > 0)
            {
                PlayAnim("UpWalk");
            }
            else if (vertical < 0)
            {
                PlayAnim("DownWalk");
            }
        }
        if ((horizontal == 0f) && (vertical == 0f))
        {
            animator.Play(currentState);
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    void Update()
    {
        teacherRenderer.sortingOrder = shadowRenderer.sortingOrder;
    }
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "LayerBack")
        {
            shadowRenderer.sortingOrder = -6;
        }
        else if (collider.tag == "LayerMiddle")
        {
            shadowRenderer.sortingOrder = -4;
        }
        else if (collider.tag == "LayerForward")
        {
            shadowRenderer.sortingOrder = -2;
        }
        else if (collider.tag == "LayerFront")
        {
            shadowRenderer.sortingOrder = 0;
        }
    }
}