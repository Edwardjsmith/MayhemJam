
using System.Collections;
using UnityEngine;

public class FPSBody : MonoBehaviour
{
    float movementSpeed = 10;
    CharacterController controller;
    bool isJumping = false;
    public AnimationCurve jumpFallOff;
    float jumpMultiplier = 10;


    // Use this for initialization
    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementInput();
        jumpInput();
    }

    void movementInput()
    {
        float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
        float vertInput = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        controller.SimpleMove(forwardMovement + rightMovement);
    }

    void jumpInput()
    {
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(jump());
        }
    }

    IEnumerator jump()
    {
        float timeInAir = 0;
        
        do
        {
            float force = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * force * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;

        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
}
