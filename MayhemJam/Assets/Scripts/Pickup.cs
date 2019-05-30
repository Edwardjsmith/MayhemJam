using UnityEngine;

public class Pickup : MonoBehaviour
{
    Transform parent;
    Collider col;
    Rigidbody rigid;

    private void Start()
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        parent = GameObject.Find("Player").transform;
        Debug.Log(parent);
    }

    private void OnMouseDown()                
    {
        col.enabled = false;
        rigid.useGravity = false;
        transform.position = parent.transform.position + parent.forward * 2;
        transform.parent = parent;
    }

    private void OnMouseUp()
    {
        col.enabled = true;
        rigid.useGravity = true;
        transform.parent = null;
    }
}
