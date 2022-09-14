using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new("treatPatient", 1, false);
        goals.Add(s1, 3);
    }
}
