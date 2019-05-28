using UnityEngine;
/// <summary>
/// Stores all Conditions of the Decision Tree
/// </summary>
class DecisionTreeConditionList
{
    /// <summary>
    /// Checks if agent has any Flag in the Inventory
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static bool TestCondition(DecisionTreeBlackboard blackboard)
    {
        return blackboard.m_debugMode;   
    }




}