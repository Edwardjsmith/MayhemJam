
using UnityEngine;

public class FPSBody : MonoBehaviour {
    public delegate void SoundTrigger(Vector3 playerPos);
    public static event SoundTrigger playerSound;
    public float jumpHeight = 7f;
    public bool isGrounded;

    private Rigidbody rb;
    public float movementSpeed = 10;
    CharacterController controller;

    float openDistance = 5.0f;

    public float y_v = 0.0f;
    public float x_v = 0.0f;
    float z_v = 0.0f;

    public float drag = 0.5f;
    public float gravity = 0.5f;

    public float jump_force = 10.0f;
    public float movement_force = 1.0f;

    public float max_speed = 10.0f;

    public float dead_space = 0.5f;
    public float target_x;
    public float target_y;
    public LayerMask rayLayer;

    // Use this for initialization
    public void Start() {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (Mathf.Abs(x_v) > drag) {
            if (x_v > 0.0f) {
                x_v -= drag;
            } else {
                x_v += drag;
            }
        } else {
            x_v = 0.0f;
        }
        if (Mathf.Abs(y_v) > drag) {
            if (y_v > 0.0f) {
                y_v -= drag;
            } else {
                y_v += drag;
            }
        } else {
            y_v = 0.0f;
        }

        target_x = Input.GetAxis("Horizontal") * max_speed;
        target_y = Input.GetAxis("Vertical") * max_speed;

        if (target_x < x_v && Mathf.Abs(target_x) > dead_space) {
            x_v += movement_force;
        } else {
            if (Mathf.Abs(target_x) > dead_space) {
                x_v -= movement_force;
            }
        }
        if (target_y < x_v && Mathf.Abs(target_y) > dead_space) {
            y_v += movement_force;
        } else {
            if (Mathf.Abs(target_y) > dead_space) {
                y_v -= movement_force;
            }
        }
        Vector3 forwardMovement = transform.forward * -y_v;
        Vector3 rightMovement = transform.right * -x_v;

        controller.SimpleMove(forwardMovement + rightMovement);
        /*
           float horizInput = Input.GetAxis("Horizontal") * movementSpeed;
           float vertInput = Input.GetAxis("Vertical") * movementSpeed;

           Vector3 forwardMovement = transform.forward * vertInput;
           Vector3 rightMovement = transform.right * horizInput;

           controller.SimpleMove(forwardMovement + rightMovement);
       */
        if (isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                rb.AddForce(Vector3.up * jumpHeight);
            }
        }
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
}
