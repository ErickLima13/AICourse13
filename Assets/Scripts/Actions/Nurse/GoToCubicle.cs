using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{
    public override bool PrePerform()
    {
        //GameObject cubicle = GameObject.FindGameObjectWithTag("Cubicle");
        //GWorld.Instance.AddCubicles(cubicle);

        //target = GWorld.Instance.RemoveCubicle();

        //if (target == null)
        //{
        //    return false;
        //}


        return true;
    }

    public override bool PostPerform()
    {
       

        return true;
    }
}
