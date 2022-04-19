using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    StudentBehaviour behaviour;
    BoxCollider2D boxCollider;
    BoxCollider2D triggerBox;
    private bool isTouching;

    private void Start()
    {
        behaviour = GetComponentInParent<StudentBehaviour>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        triggerBox = GetComponent<BoxCollider2D>();
        Vector2 triggerSize = new Vector2(1.1f, 1.15f);
        triggerSize = Vector2.Scale(triggerSize, boxCollider.size);
        triggerBox.size = triggerSize;
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
