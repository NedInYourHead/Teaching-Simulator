using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToForeground : MonoBehaviour
{
    private int teacherSortOrder = 0;
    [SerializeField] private GameObject student;
    [SerializeField] private SpriteRenderer[] childrenRenderers;
    void Start()
    {
        student = transform.parent.gameObject;
        childrenRenderers = student.GetComponentsInChildren<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Teacher")
        {
            for (int i = 0; i < childrenRenderers.Length; i++)
            {
                childrenRenderers[i].sortingOrder = teacherSortOrder + 1;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Teacher")
        {
            for (int i = 0; i < childrenRenderers.Length; i++)
            {
                childrenRenderers[i].sortingOrder = teacherSortOrder - 1;
            }
        }
    }
}
