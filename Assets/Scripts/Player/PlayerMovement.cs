
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private Transform orientation;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float floorCheckDistance = 1f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private LayerMask floorMask;
    private bool hasJustJumped = false;

    private CharacterController controller;
    private Vector2 input;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
            InputManager.Instance.OnJumpInitiated += Jump;
    }

    private void Update()
    {
        if (InputManager.Instance != null)
        {
            input = InputManager.Instance.GetMovement();
        }
        else
        {
            input = Vector2.zero;
        }

        Vector3 move = orientation.right * input.x + orientation.forward * input.y;

        move *= moveSpeed;

        if (IsGrounded())
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }
     
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = move;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    private void Jump()
    {

            Debug.DrawLine(orientation.position, Vector3.down, Color.red);
            verticalVelocity += jumpForce;
        
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpInitiated -= Jump;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(orientation.position, Vector3.down, floorCheckDistance, floorMask);
    }
}



