using UnityEngine;
using UnityEngine.InputSystem;

// このスクリプトは、Unityの新しいInput Systemパッケージがインストールされていることを前提とします。
// PlayerオブジェクトにPlayerInputコンポーネントをアタッチし、
// "Move" (Vector2), "Look" (Vector2), "Sprint" (Button) のアクションを設定してください。

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5.0f;
    public float runSpeedMultiplier = 1.8f;
    public float mouseSensitivity = 0.1f;
    public float gamepadSensitivity = 100.0f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isSprinting = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    // PlayerInputコンポーネントから "Move" アクションがトリガーされたときに呼び出される
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // PlayerInputコンポーネントから "Look" アクションがトリガーされたときに呼び出される
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    // PlayerInputコンポーネントから "Sprint" アクションがトリガーされたときに呼び出される
    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    private void HandleMovement()
    {
        float currentSpeed = isSprinting ? moveSpeed * runSpeedMultiplier : moveSpeed;
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        characterController.SimpleMove(moveDirection * currentSpeed);
    }

    private void HandleLook()
    {
        // ゲームパッドとマウスで感度を切り替える
        float sensitivity = IsGamepad() ? gamepadSensitivity : mouseSensitivity;
        float lookX = lookInput.x * sensitivity * Time.deltaTime;
        float lookY = lookInput.y * sensitivity * Time.deltaTime;

        // 横方向の回転（プレイヤー本体）
        transform.Rotate(0, lookX, 0);

        // 縦方向の回転（カメラ）
        verticalRotation -= lookY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private bool IsGamepad()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null && playerInput.currentControlScheme == "Gamepad")
        {
            return true;
        }
        return false;
    }
}
