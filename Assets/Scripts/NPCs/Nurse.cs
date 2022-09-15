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

        SubGoal s2 = new("rested", 1, false);
        goals.Add(s2, 1);

        Invoke(nameof(GetTired), Random.Range(10, 20));
    }

    private void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke(nameof(GetTired), Random.Range(10, 20));
    }
}
