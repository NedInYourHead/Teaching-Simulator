using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool isTouching;

    private void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
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
        if (isTouching)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
