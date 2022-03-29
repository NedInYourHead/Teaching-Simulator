using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    private float vertical;
    [SerializeField] private float speed = 0.2f;

    private float horizontal;
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");

        horizontal = Input.GetAxis("Horizontal");

        transform.position = transform.position + (transform.up * vertical * speed);
    }
}
