using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    StudentBehaviour behaviour;
    private bool isTouching;

    private void Start()
    {
        behaviour = GetComponentInParent<StudentBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }

    void FixedUpdate()
    {
        if (isTouching && behaviour.GetBehaviour() != "normal")
        {
            behaviour.SetNormal();
        }
    }
}
