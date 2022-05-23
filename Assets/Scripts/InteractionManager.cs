using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    NewStudentBehaviour behaviour;
    BoxCollider2D triggerBox;
    private bool isTouching;
    public static float timer;

    private void Awake()
    {
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
        if (isTouching && (Input.GetAxis("Fire1") > 0) && (timer <= 0f))
        {
            timer += 0.5f;
            behaviour.Interact();
        }
        else
        {
            if((timer > 0f))
            {
                timer -= Time.deltaTime;
            }
        }
        behaviour.IconHighlight(isTouching);
    }
}
