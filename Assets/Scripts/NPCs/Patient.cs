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


        SubGoal s2 = new("isTreated", 1, true);
        goals.Add(s2, 5);

    


    }

    private void OnEnable()
    {
        
        

    }


}
