using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float rotationSpeed = 2f;


    private Vector3 velocity;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction rotateAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        rotateAction = InputSystem.actions.FindAction("Look");
        velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player in accordance to their inputs
        Vector3 moveValue = moveAction.ReadValue<Vector3>() * moveSpeed * Time.fixedDeltaTime;
        velocity += moveValue;

        // Change the camera angle in accordance to player input
        float turnHoriz = rotateAction.ReadValue<Vector2>().x * rotationSpeed * Time.fixedDeltaTime;
        float turnVert = rotateAction.ReadValue<Vector2>().y * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(turnVert, turnHoriz, 0f);


        // Move and rotate the player
        rb.transform.position += velocity;
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
