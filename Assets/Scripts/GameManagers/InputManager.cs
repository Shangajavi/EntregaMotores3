using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private MyInputSys playerInput;

    public event Action OnJumpInitiated;
    public event Action OnJumpCanceled;

    public event Action OnShootInitiated;
    public event Action OnShootCanceled;

    public event Action OnRechargeInitiated;
    public event Action OnRechargeCanceled;

    public event Action OnInteractInitiated;
    public event Action OnInteractCanceled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInput = new MyInputSys();

        playerInput.Player.Jump.started += _ => OnJumpInitiated?.Invoke();
        playerInput.Player.Jump.canceled += _ => OnJumpCanceled?.Invoke();

        playerInput.Player.Shoot.started += _ => OnShootInitiated?.Invoke();
        playerInput.Player.Shoot.canceled += _ => OnShootCanceled?.Invoke();

        playerInput.Player.Interact.started += _ => OnInteractInitiated?.Invoke();
        playerInput.Player.Interact.canceled += _ => OnInteractCanceled?.Invoke();

        playerInput.Player.Recharge.started += _ => OnRechargeInitiated?.Invoke();
        playerInput.Player.Recharge.canceled += _ => OnRechargeCanceled?.Invoke();
    }

    private void OnEnable() => playerInput.Player.Enable();
    private void OnDisable() => playerInput.Player.Disable();

    public Vector2 GetMovement()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }
}