using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement instance;

    [SerializeField] Transform playerCamera;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [Range(0, 0.1f)] public float mouseSmoothTime = 0.05f;
    [Range(0, 5)] public float mouseSensitivity = 3.5f;
    [Range(0, 0.1f)] public float moveSmoothTime = 0.05f;
    [Range(0, 20f)] public float maxStamina = 10f;
    [Range(0, 2)] public float staminaRegenSpeed = 1.5f;
    [Header("Moving Speeds")]
    [Range(0, 5)] public float walkSpeed = 3f;
    [Range(0, 5)] public float runSpeed = 3f;
    [Range(0, 5)] public float crouchSpeed = 3f;
    public float stamina;
    public float speed;
    float cameraCap;
    float velocityY;
    float gravity = -30f;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    bool isGrounded;
    public bool isRunning;
    public bool isCrouching;
    CharacterController controller;
    Transform playerTransform;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        stamina = maxStamina;
        playerTransform = this.gameObject.transform;
    }

    void Update()
    {
        updateMove();
        updateMouse();
        updateStamina();
    }

    void updateMove()
    {
        if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
        else isRunning = false;
        if (Input.GetKey(KeyCode.LeftControl) && !isRunning)
        {
            isCrouching = true;
            speed = crouchSpeed;
            playerTransform.localScale = new Vector3(playerTransform.localScale.x, 0.3f, playerTransform.localScale.z);
        }
        else if (!isRunning)
        {
            isCrouching = false;
            speed = walkSpeed;
            playerTransform.localScale = new Vector3(playerTransform.localScale.x, 0.6f, playerTransform.localScale.z);
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        velocityY += gravity * 2 * Time.deltaTime;
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }

    }

    void updateStamina()
    {
        if (isRunning && !isCrouching)
        {
            if (stamina > 0)
            {
                speed = runSpeed;
                stamina -= Time.deltaTime;
            }
            else speed = walkSpeed;
        }

        if (stamina < maxStamina && !isRunning)
        {
            stamina += Time.deltaTime * staminaRegenSpeed;
            speed = walkSpeed;
        }
    }

    void updateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90, 90);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
}
