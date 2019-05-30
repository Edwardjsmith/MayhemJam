using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseScript : MonoBehaviour {

    private SphereCollider m_collider;

    private void Start()
    {
        m_collider = GetComponent<SphereCollider>();
    }

    void CreateNoise(float range)
    {
        GetComponent<SphereCollider>().radius = range;
    }

    private void Update()
    {
        if(m_collider.radius > 0)
        {
            m_collider.radius = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HearingAIScript>() != null)
            other.gameObject.GetComponent<HearingAIScript>().positions.Add(transform.position);
    }




}
