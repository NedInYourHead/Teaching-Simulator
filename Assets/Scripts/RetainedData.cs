using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RetainedData
{
    /*
    Student list contents
    0. float - curriculum level (increment based on learning points)
    1. bool - has the hook been discovered yet
    2. int - talking change during lesson
    3. int - sleeping change during lesson
    4. int - hand up shyness change
    5. string - ink json file to use
    */

    private static int totalDays = 20;
    public static int TotalDays { get { return totalDays; } }


    public static float[] studentCurriculum = new float[9];
    public static bool[] hookDiscovered = new bool[9];

    public static void Reset()
    {
        studentCurriculum = new float[9];
        hookDiscovered = new bool[9];
    }
}