using System;
using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [field: NonSerialized] public float moveAmount { get; private set; } 
    [field: SerializeField] public Vector2 movementInput { get; private set; }
    [field: NonSerialized] public float verticalInput { get; private set; }
    [field: NonSerialized] public float horizontalInput { get; private set; }
    private float moveSpeed = 6f;

    [field: NonSerialized] public bool jumpInput { get; private set; }
    private float groundCheckRadius = 0.2f;
    private float jumpForce = 5f;
    private bool canJump = true;

    private Rigidbody playerRigidbody;
    [SerializeField] private bool isGrounded = true;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody>();

        // Подписка на инпут перемещения
        playerInput.PlayerLocomotion.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        playerInput.PlayerLocomotion.Movement.canceled += _ => movementInput = Vector2.zero;

        // Подписка на инпут прыжка
        playerInput.PlayerLocomotion.Jump.performed += context => jumpInput = true;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpingInput();
    }

    // =============== Передвижение ===============

    // Основное передвижение WASD
    private void HandleMovementInput()
    {
        float moveHorizontal = movementInput.x;
        float moveVertical = movementInput.y;

        Vector3 currentVelocity = playerRigidbody.linearVelocity;
        Vector3 movementVelocity = moveSpeed * new Vector3(moveHorizontal, 0, moveVertical);

        // Обновляем только горизонтальные компоненты скорости
        playerRigidbody.linearVelocity = new Vector3(movementVelocity.x, currentVelocity.y, movementVelocity.z);

    }

    // Прыжок
    private void HandleJumpingInput()
    {
        // Есть ли вблизи с персонажем земля
        if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit,
            1f, LayerMask.GetMask("Default")))
            isGrounded = true;

        if (jumpInput && isGrounded)
            Jump();
    }

    public void Jump()
    {
        if (!canJump)
            return;

        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpInput = false; // Сбрасываем флаг прыжка
        isGrounded = false;

        // Кулдаун на прыжок
        StartCoroutine(JumpCooldown(1f));
    }

    private IEnumerator JumpCooldown(float cooldownTime)
    {
        canJump = false;
        yield return new WaitForSeconds(cooldownTime);
        canJump = true;
    }
}
