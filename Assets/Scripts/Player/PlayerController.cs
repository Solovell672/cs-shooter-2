using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    
    private CharacterController characterController;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Camera mainCamera;
    private bool isGrounded;
    private float currentSpeed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleCamera();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime + velocity * Time.deltaTime);
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleJump()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
        }

        velocity.y -= 9.81f * Time.deltaTime;
    }

    public bool IsGrounded => isGrounded;
    public Vector3 GetVelocity => velocity;
}