using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    StudentBehaviour behaviour;
    BoxCollider2D triggerBox;
    private bool isTouching;

    private void Start()
    {
        behaviour = GetComponentInParent<StudentBehaviour>();
        triggerBox = GetComponent<BoxCollider2D>();
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
        if (isTouching && behaviour.GetBehaviour() != "normal" && (Input.GetAxis("Fire1") > 0))
        {
            behaviour.SetNormal();
        }
        behaviour.IconHighlight(isTouching);
    }
}
