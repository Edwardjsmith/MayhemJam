using UnityEngine;
/// <summary>
/// Stores all Actions for the Decision Tree
/// </summary>
class DecisionTreeActionList
{
    /// <summary>
    /// Moves to a random Location
    /// </summary>
    /// <param name="root"></param>
    public static void TestAction(DecisionTreeBlackboard blackboard)
    {
        Debug.Log("Working" + ((blackboard.m_debugMode)? "1": "2"));
    }




}
