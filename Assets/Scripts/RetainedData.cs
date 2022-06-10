using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RetainedData
{
    private static int totalDays = 20;
    public static int TotalDays { get { return totalDays; } }
    public static float[] studentCurriculum = new float[9];
    public static bool[] hookDiscovered = new bool[9];
    public static string[] studentNames = new string[9];

    public static void Reset()
    {
        studentCurriculum = new float[9];
        hookDiscovered = new bool[9];
        studentNames = new string[9];
    }
}