using UnityEngine;
using UnityEngine.InputSystem;

// このスクリプトは、Unityの新しいInput Systemパッケージがインストールされていることを前提とします。
// PlayerオブジェクトにPlayerInputコンポーネントをアタッチし、
// "Move", "Look", "Sprint" のアクションを設定してください。
// スマホ操作の場合は、OnScreenJoystickとUIボタンをシーンに配置する必要があります。

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5.0f;
    public float runSpeedMultiplier = 1.8f;
    public float mouseSensitivity = 0.1f;
    public float gamepadSensitivity = 100.0f;
    public float touchLookSensitivity = 0.5f;

    [Header("Mobile UI (Optional)")]
    public OnScreenJoystick joystick;
    public TouchLookArea touchLookArea;

    private CharacterController characterController;
    private Camera playerCamera;
    private PlayerInput playerInput;
    private float verticalRotation = 0f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isSprinting = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleLook();
    }

    private void HandleInput()
    {
        // スマホ操作の場合、UIから入力を受け取る
        if (IsMobile())
        {
            if (joystick != null)
            {
                moveInput = joystick.InputDirection;
            }
            if (touchLookArea != null)
            {
                lookInput = touchLookArea.LookInput;
            }
        }
    }

    // --- Input System Events (for PC/Gamepad) ---
    public void OnMove(InputValue value)
    {
        if (!IsMobile()) moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (!IsMobile()) lookInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        if (!IsMobile()) isSprinting = value.isPressed;
    }

    // --- UI Button Events (for Mobile) ---
    public void SetSprinting(bool sprinting)
    {
        if (IsMobile()) isSprinting = sprinting;
    }

    private void HandleMovement()
    {
        float currentSpeed = isSprinting ? moveSpeed * runSpeedMultiplier : moveSpeed;
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        characterController.SimpleMove(moveDirection * currentSpeed);
    }

    private void HandleLook()
    {
        float sensitivity = GetCurrentLookSensitivity();
        float lookX = lookInput.x * sensitivity * Time.deltaTime;
        float lookY = lookInput.y * sensitivity * Time.deltaTime;

        transform.Rotate(0, lookX, 0);

        verticalRotation -= lookY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private bool IsMobile()
    {
        // PlayerInputの現在のControl SchemeでPCかスマホかを判定
        return playerInput.currentControlScheme == "Touch";
    }

    private float GetCurrentLookSensitivity()
    {
        string scheme = playerInput.currentControlScheme;
        switch (scheme)
        {
            case "Gamepad":
                return gamepadSensitivity;
            case "Touch":
                return touchLookSensitivity;
            case "Keyboard&Mouse":
            default:
                return mouseSensitivity;
        }
    }
}
