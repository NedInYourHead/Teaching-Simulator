using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StudentProfiles
{
    [SerializeField] private static TextAsset[] inkJsons;
    

    public class Student
    {
        public string name;
        public int curriculumLevel;
        public int inkNum;
        public bool hookDiscovered;
        public int maxTalkChance;
        public int maxSleepChance;
        public int minLearningSpeed;
    }
}
