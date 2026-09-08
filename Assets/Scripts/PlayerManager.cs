using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerManager : MonoBehaviour
{
    public float jumpForce = 5;

    public GameManager.Gamemode startGameMode = GameManager.Gamemode.Cube;

    [SerializeField] private Sprite cubeSprite, shipSprite;
    private SpriteRenderer spriteRenderer;
    public GameManager.Gamemode CurrMode { get; private set; }

    

    [SerializeField] private LayerMask groundLayer, spikesLayer;
    private Rigidbody2D rb;
    private bool isGrounded;

    private InputAction jumpAction;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        jumpAction = InputSystem.actions.FindAction("Jump");

        SwitchGamemode(startGameMode);
    }

    private void Update()
    {
        if (jumpAction.IsPressed() && (CurrMode == GameManager.Gamemode.Ship || isGrounded))
        {
            //print("Jump!");

            // Reset Y velocity for consistent jump height
            rb.linearVelocityY = 0;

            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if colliding with the ground
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
    }

    public void SwitchGamemode(GameManager.Gamemode gamemode)
    {
        CurrMode = gamemode;

        if(gamemode == GameManager.Gamemode.Cube)
        {
            spriteRenderer.sprite = cubeSprite;
        }
        else if (gamemode == GameManager.Gamemode.Ship)
        {
            spriteRenderer.sprite = shipSprite;
        }
    }
}
