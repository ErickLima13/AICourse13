using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string,int> allstates,GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new(allstates);
        this.action = action;
    }

    public Node(Node parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new(allstates);
        
        foreach(KeyValuePair<string,int> b in beliefstates)
        {
            if (!this.state.ContainsKey(b.Key))
            {
                this.state.Add(b.Key, b.Value);
            }
        }

        this.action = action;
    }
}

public class GPlanner
{
   public Queue<GAction> plan(List<GAction> actions,Dictionary<string,int> goal,WorldStates beliefstates)
    {
        List<GAction> usableActions = new();

        foreach(GAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new();
        Node start = new(null, 0, GWorld.Instance.GetWorld().GetStates(),beliefstates.GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;

        foreach(Node leaf in leaves)
        {
            if(cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if(leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<GAction> result = new();
        Node n = cheapest;

        while(n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }

            n = n.parent;
        }

        Queue<GAction> queue = new();

        foreach(GAction a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("the plan is: ");

        foreach(GAction a in queue)
        {
            Debug.Log("Q: " + a.actioName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent,List<Node> leaves,List<GAction> usuableActions,Dictionary<string,int> goal)
    {
        bool foundPath = false;

        foreach(GAction action in usuableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new(parent.state);

                foreach(KeyValuePair<string,int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                    {
                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                Node node = new(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubSet(usuableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }

        return foundPath;
    }

    private bool GoalAchieved(Dictionary<string,int> goal,Dictionary<string,int> state)
    {
        foreach(KeyValuePair<string,int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }

        return true;
    }

    private List<GAction> ActionSubSet(List<GAction> actions,GAction removeMe)
    {
        List<GAction> subset = new();

        foreach(GAction a in actions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }

        return subset;
    }

}
