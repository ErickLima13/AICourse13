using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    private SubGoal currentGoal;

    private Queue<GAction> actionQueue;

    private bool invoked;

    public List<GAction> actions = new();
    public Dictionary<SubGoal, int> goals = new();

    GPlanner planner;

    public GAction currentAction;


    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = GetComponents<GAction>();

        foreach (GAction a in acts)
        {
            actions.Add(a);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < 1f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }

            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, null);

                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();

            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);
                }

                if (currentAction.targetTag != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }

    }

    private void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }
}
