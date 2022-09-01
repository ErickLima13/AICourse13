using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actioName = "Action";

    public float cost = 1.0f;
    public float duration;

    public GameObject target;
    public string targetTag;
    
    public WorldState[] preConditions;
    public WorldState[] afterEffects;

    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running;

    public GAction()
    {
        preconditions = new();
        effects = new();
    }

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if(preConditions != null)
        {
            foreach(WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }
    
    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string,int> conditions)
    {
        foreach(KeyValuePair<string,int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }

        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
