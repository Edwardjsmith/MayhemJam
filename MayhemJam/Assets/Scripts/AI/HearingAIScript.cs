using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingAIScript : MonoBehaviour {

    public List<Vector3> positions;

    private void Update()
    {
        if (positions.Count > 3)
            positions.RemoveAt(0);
    }




}
