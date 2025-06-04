using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 2f;
    public float resetSpeedPercent = 1f;
    public float maxSpeed = 100f;
    static public bool isLanded = true;
    static public Vector3 closestBodyDistance;


    private Vector3 velocity;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction rotateAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = 1000;
        moveAction = InputSystem.actions.FindAction("Move");
        rotateAction = InputSystem.actions.FindAction("Look");
        velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (InputSystem.actions.FindAction("ResetVelocity").IsPressed())
        {
            if (velocity.magnitude > 10)
            {
                velocity -= velocity * (resetSpeedPercent / 100);
            }
            else
            {
                velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        // Move the player in accordance to their inputs
        Vector3 moveValue = rb.rotation * moveAction.ReadValue<Vector3>() * moveSpeed * Time.fixedDeltaTime;

        // Change the camera angle in accordance to player input
        float turnHoriz = rotateAction.ReadValue<Vector2>().x * rotationSpeed * Time.fixedDeltaTime;
        float turnVert = rotateAction.ReadValue<Vector2>().y * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(turnVert, turnHoriz, 0f);


        velocity += moveValue * rb.mass;
        rb.MoveRotation(rb.rotation * turnRotation);
        rb.position += velocity;

        // If the player velocity exceeds maximum, set it to the maximum
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity * (maxSpeed / velocity.magnitude);
        }

        // Update the camera so that downwards is in the direction of the ground (of the closest celestial body assuming within 500m of it's surface)
        if (closestBodyDistance.magnitude < 500)
        {
            // Rotate the camera
            rb.rotation = Quaternion.FromToRotation(transform.up, closestBodyDistance) * rb.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (velocity.sqrMagnitude <= 160000 && other.GetComponent<Rigidbody>() != null)
        {
            isLanded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isLanded = false;
    }
}
