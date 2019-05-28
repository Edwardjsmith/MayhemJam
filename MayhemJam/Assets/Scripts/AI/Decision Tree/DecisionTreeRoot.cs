using System.Collections;
using System.Collections.Generic;

public delegate void Action(DecisionTreeBlackboard blackboard);
public delegate bool Condition(DecisionTreeBlackboard blackboard);

public class DecisionTreeRoot {

    #region Variables
    private DecisionTreeNode m_decisionTreeRoot;
    
    #endregion Variables
    #region  Properties

    #endregion Properties

    #region Constructor
    /// <summary>
    /// Constructor for Decision Tree
    /// </summary>
    /// <param name="agent"></param>
    /// <param name="inventory"></param>
    /// <param name="actions"></param>
    /// <param name="data"></param>
    /// <param name="sensing"></param>
    /// <param name="debugging"></param>
    public DecisionTreeRoot(DecisionTreeBlackboard blackboard, bool debugging = false)
    {

        blackboard.m_debugMode = debugging;
        // setting variables that can be retrieved via properties

        Condition testCondition = new Condition(DecisionTreeConditionList.TestCondition);

        Action testAction = new Action(DecisionTreeActionList.TestAction);

        DecisionTreeAction testDTAction = new DecisionTreeAction("Test Action", blackboard, testAction);

        DecisionTreeCondition testDTCondtition = new DecisionTreeCondition("Test Condition", blackboard, testCondition, testDTAction, testDTAction);

        m_decisionTreeRoot = testDTCondtition;
        //setting all the condition
        //Condition flagInSight                       = new Condition(DecisionTreeConditionList.IsFlagInSight);
        //Condition isEnemyInSight                    = new Condition(DecisionTreeConditionList.IsEnemyInSight);
        //Condition hasPowerUp                        = new Condition(DecisionTreeConditionList.HasPowerUp);
        //Condition isItemInView                      = new Condition(DecisionTreeConditionList.HasCollectableInSight);
        //Condition hasFlagInInventory                = new Condition(DecisionTreeConditionList.HasFlagInInventory);
        //Condition isFriendlyTooClose                = new Condition(DecisionTreeConditionList.IsFriendlyTooClose);
        //Condition hasHalfHPandHealthKit             = new Condition(DecisionTreeConditionList.IsHalfHPWithHealthKit);
        //Condition isEnemyNearFriendlyFlagAndBase    = new Condition(DecisionTreeConditionList.IsEnemyCloseToFriendlyFlagAndBase);

        ////setting up all actions
        //Action moveRand         = new Action(DecisionTreeActionList.MoveToRandomLocation);
        //Action attackEnem       = new Action(DecisionTreeActionList.AttackEnemyInSight);
        //Action collectFlag      = new Action(DecisionTreeActionList.CollectFlagInSight);
        //Action collectItem      = new Action(DecisionTreeActionList.CollectItemInSight);
        //Action usePowerUp       = new Action(DecisionTreeActionList.UsePowerUp);
        //Action returnFlagToBase = new Action(DecisionTreeActionList.ReturnFlagToBase);
        //Action avoidFriendly    = new Action(DecisionTreeActionList.AvoidFriendly);
        //Action useHealthkit     = new Action(DecisionTreeActionList.UseHealthKit);
        //Action interceptEnemy   = new Action(DecisionTreeActionList.InterceptEnemy);


        //// Creating the decision tree leaf nodes aka Actions
        //DecisionTreeAction moveRandomlyDTAction         = new DecisionTreeAction("Random", this, moveRand);
        //DecisionTreeAction attackEnemyDTAction          = new DecisionTreeAction("Random", this, attackEnem);
        //DecisionTreeAction collectFlagDTAction          = new DecisionTreeAction("Collecting Flag", this, collectFlag);
        //DecisionTreeAction collectItemDTAction          = new DecisionTreeAction("Collectin Item", this, collectItem);
        //DecisionTreeAction usePowerUpDTAction           = new DecisionTreeAction("Using Power Up", this, usePowerUp);
        //DecisionTreeAction returnFlagToBaseDTAction     = new DecisionTreeAction("Return to Base", this, returnFlagToBase);
        //DecisionTreeAction avoidFriendlyDTAction        = new DecisionTreeAction("Avoid Friendly", this, avoidFriendly);
        //DecisionTreeAction useHealthKitDTAction         = new DecisionTreeAction("Use Health kit at low HP", this, useHealthkit);
        //DecisionTreeAction interceptEnemyDTAction       = new DecisionTreeAction("Intercepting Enemy", this, interceptEnemy);


        //// Creating the decision tree nodes.
        ////Note that the order is important as you have to add the child nodes before the node exists.
        ////this way the decision tree is build up from its deepest level to its highest
        //DecisionTreeCondition isFriendlyTooCloseDTCondition     = new DecisionTreeCondition("Is Friendly too close", this, isFriendlyTooClose, moveRandomlyDTAction, avoidFriendlyDTAction);
        //DecisionTreeCondition powerUpInInventoryDTCondition     = new DecisionTreeCondition("Power Up In Inventory", this, hasPowerUp, attackEnemyDTAction, usePowerUpDTAction);
        //DecisionTreeCondition collectableInViewDTCondition      = new DecisionTreeCondition("CollectableInViewDTCondition", this, isItemInView, isFriendlyTooCloseDTCondition, collectItemDTAction);
        //DecisionTreeCondition enemyNearFlagAndBaseDTCondition   = new DecisionTreeCondition("Checking if Enemy is near Friendly Flag", this, isEnemyNearFriendlyFlagAndBase, powerUpInInventoryDTCondition, interceptEnemyDTAction);
        //DecisionTreeCondition enemyInSightDTCondition           = new DecisionTreeCondition("Enemy In Sight", this, isEnemyInSight, collectableInViewDTCondition, enemyNearFlagAndBaseDTCondition);
        //DecisionTreeCondition hasFlagInSightDTCondition         = new DecisionTreeCondition("Check For Enemy Flag", this, flagInSight, enemyInSightDTCondition, collectFlagDTAction);

        //DecisionTreeCondition hasFlagInInventoryDTCondition     = new DecisionTreeCondition("Has Flag In Inventory", this, hasFlagInInventory, hasFlagInSightDTCondition, returnFlagToBaseDTAction);
        //DecisionTreeCondition hasLowHPandHealthKitDTCondition   = new DecisionTreeCondition("Check Health and search Inventory for Health Kit", this, hasHalfHPandHealthKit, hasFlagInInventoryDTCondition, useHealthKitDTAction);



        ////Set a starting point for the decision tree
        //m_decisionTreeRoot = hasLowHPandHealthKitDTCondition;

    }

    #endregion Constructor



    /// <summary>
    /// Run through the decision tree to take action
    /// </summary>
    public void Traverse()
    {
        m_decisionTreeRoot.Run();
    }
    


}
