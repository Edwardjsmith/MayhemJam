using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionTreeNode {
    
    public string m_name;
    public DecisionTreeBlackboard m_blackboard;

    public DecisionTreeNode(string name, DecisionTreeBlackboard blackboard)
    {
        m_name = name;
        m_blackboard = blackboard;
    }

    public abstract void Run();

    public override string ToString()
    {
        return "Node: " + m_name;
    }

}
