using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionAIScript : MonoBehaviour {

    public List<GameObject> objectsInSight = new List<GameObject>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "untagged" && objectsInSight.Contains(other.gameObject))
            objectsInSight.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "untagged")
            objectsInSight.Remove(other.gameObject);
    }

}
