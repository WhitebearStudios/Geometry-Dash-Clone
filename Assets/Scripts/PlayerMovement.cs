using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5;
    const float rotationSpeed = -180 / 0.7f;


    private PlayerManager manager;
    [SerializeField] private Transform visualsTransform;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private InputAction jumpAction;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();

        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (jumpAction.IsPressed() && (manager.CurrMode == GameManager.Gamemode.Ship || isGrounded))
        {
            //print("Jump!");

            // Reset Y velocity for consistent jump height
            rb.linearVelocityY = 0;

            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (!isGrounded)
        {
            float newRot = visualsTransform.eulerAngles.z + rotationSpeed * Time.deltaTime; 
            visualsTransform.eulerAngles = Vector3.forward * newRot;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if colliding with the ground
        if ((manager.groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;

            //Snap to nearest 90 degrees
            float currRot = visualsTransform.eulerAngles.z;
            float nearest90 = Mathf.Round(currRot / 90) * 90;

            visualsTransform.eulerAngles = Vector3.forward * nearest90;
        }
    }
}
