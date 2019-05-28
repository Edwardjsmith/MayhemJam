
using UnityEngine;

public class FPSBody : MonoBehaviour
{
    public delegate void SoundTrigger(Vector3 playerPos);
    public static event SoundTrigger playerSound;

    public float movementSpeed = 10;
    CharacterController controller;

    float openDistance = 5.0f;

    public LayerMask rayLayer;

	// Use this for initialization
	public void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
        float vertInput = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        controller.SimpleMove(forwardMovement + rightMovement);
    }
}
