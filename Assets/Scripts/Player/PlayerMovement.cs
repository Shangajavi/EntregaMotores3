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
    [SerializeField] private float verticalVelocity;

    private CharacterController controller;
    [SerializeField]private bool floor;
    private Vector2 input;

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
        floor = IsGrounded();
        Vector3 move = orientation.right * input.x + orientation.forward * input.y;
        move *= moveSpeed;

        if (floor)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = 0.01f;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    private void Jump()
    {
        Debug.DrawLine(orientation.position, orientation.position + Vector3.down * floorCheckDistance, Color.red, 0.2f);
        if (!floor)
        {
            return;
        }

        if (floor)
        {
            Debug.Log("JUMP EVENT");
            verticalVelocity = jumpForce;
        }
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