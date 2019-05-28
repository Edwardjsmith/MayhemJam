using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTreeCondition : DecisionTreeNode
{
    private DecisionTreeNode m_rightNode;
    private DecisionTreeNode m_leftNode;

    
    Condition m_conditon;


    public DecisionTreeCondition(string name, DecisionTreeBlackboard blackboard, Condition condition, DecisionTreeNode left, DecisionTreeNode right) : base(name, blackboard)
    {
        m_conditon = condition;
        m_rightNode = right;
        m_leftNode = left;
    }


    public override void Run()
    {
        //Uncomment this to get further testing information
        //if (m_root.m_debugging)
        //    Debug.Log(this.ToString());

        if (m_conditon.Invoke(m_blackboard))
        {
            m_rightNode.Run();
        }
        else
        {
            m_leftNode.Run();
        }
    }
}
