using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    new void Start()
    {
        base.Start();
        SubGoal s1 = new("isWaiting", 1, true);
        goals.Add(s1, 3);
    }




}
