using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloatingNumber : MonoBehaviour
{
    [SerializeField] private GameObject floatingPoints;

    void SpawnPoints(string s)
    {
        Instantiate(floatingPoints, transform.position, Quaternion.identity);
    }
}
