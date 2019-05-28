using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIScript : MonoBehaviour {

    DecisionTreeRoot m_decisionTreeRoot;
    DecisionTreeBlackboard m_blackboard;


	// Use this for initialization
	void Start () {
        m_blackboard = new DecisionTreeBlackboard();
        m_decisionTreeRoot = new DecisionTreeRoot(m_blackboard);

	}
	
	// Update is called once per frame
	void Update () {
        m_decisionTreeRoot.Traverse();
	}
}
