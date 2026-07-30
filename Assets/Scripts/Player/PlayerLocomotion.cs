using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotion : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 5f;

    [Header("Jump/Gravity")]
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _controller;
    private PlayerInputActions _inputActions;
    private Health _health;
    private Transform _cameraTransform;
    private Vector2 _moveInput;
    private float _verticalVelocity;
    private bool _isSprinting;

    public static event Action OnPlayerDied;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputActions = new PlayerInputActions();
        _health = GetComponent<Health>();
        _cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Jump.performed += OnJumpPerformed;
        _inputActions.Player.Sprint.performed += OnSprintPerformed;
        _inputActions.Player.Sprint.canceled += OnSprintCanceled;
        _health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
        _inputActions.Player.Sprint.performed -= OnSprintPerformed;
        _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
        _health.OnDeath -= HandleDeath;
        _inputActions.Player.Disable();
    }

    private void HandleDeath()
    {
        Debug.Log("Player has died.");
        enabled = false;
        OnPlayerDied?.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        _isSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _isSprinting = false;
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_controller.isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        ApplyRotation(moveDirection);
        ApplyGravity();

        float speed = _isSprinting ? _sprintSpeed : _moveSpeed;
        Vector3 velocity = moveDirection * speed;
        velocity.y = _verticalVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;

        camForward.y = 0f; // Làm phẳng vector forward xuống mặt phẳng XZ
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * _moveInput.y + camRight * _moveInput.x;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            return moveDirection.normalized; // có input đủ lớn → trả hướng đã chuẩn hoá
        }
        else
        {
            return Vector3.zero; // input quá nhỏ / không có → đứng yên
        }
    }

    private void ApplyRotation(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude < 0.01f)
        {
            return; // input quá nhỏ / không có → đứng yên, không xoay

        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f; // reset về một giá trị âm nhỏ
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
}