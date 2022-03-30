using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    private string movePriority;


    private float vertical;
    [SerializeField] private float speed = 0.2f;

    private float horizontal;
    
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
        }
        if (movePriority == "y" || horizontal == 0f)
        {
            transform.position = transform.position + (transform.up * vertical * speed);
        }
    }
}
