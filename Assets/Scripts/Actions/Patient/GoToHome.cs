using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHome : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        SpawnerPool.pool.Release(gameObject);
        print("fui pra casa");
        //gameObject.SetActive(false);
        
        return true;
    }

}
