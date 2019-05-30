using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTreeBlackboard {

    public bool m_debugMode;

    public HearingAIScript m_hearingSense;
    public VisionAIScript m_viewingSense;

    public Vector3 [] m_patrollPoints;
    private int m_patrollIndex;

    public int PatrollIndex
    {
        get
        {
            return m_patrollIndex % m_patrollPoints.Length;
        }

        set
        {
            m_patrollIndex = value;
        }
    }
}
