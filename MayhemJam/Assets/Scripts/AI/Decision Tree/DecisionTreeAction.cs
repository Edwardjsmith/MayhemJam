using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionTreeAction : DecisionTreeNode
{
    Action action;    
    private DecisionTreeActionList m_action;

    private DecisionTreeBlackboard m_blackboard;

    public DecisionTreeAction(string name, DecisionTreeBlackboard blackboard, Action action) : base(name, blackboard)
    {
        m_blackboard = blackboard;
        this.action = action;
    }

    

    public override void Run()
    {
        //Uncomment this to get further testing information
        //if (m_root.m_debugging)
        //    Debug.Log(this.ToString());
        action.Invoke(m_blackboard);
    }
}
