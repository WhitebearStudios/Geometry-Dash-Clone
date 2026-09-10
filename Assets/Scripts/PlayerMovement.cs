using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(PlayerManager))]
public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5;
    const float rotationSpeed = -180 / 0.5f;
    public float shipAccel = 100, maxShipVel = 15;

    [SerializeField] private TileBase coinTile;
    [SerializeField] private TileBase shipPortalTileTop, shipPortalTileBottom, cubePortalTileTop, cubePortalTileBottom;
    [SerializeField] Tilemap specialTiles;


    private PlayerManager manager;
    [SerializeField] private Transform visualsTransform;

    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    private bool isGrounded = true;

    private InputAction jumpAction;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    public void ResetMovement()
    {
        isGrounded = true;
        transform.position = GameManager.playerStartPos;
    }

    private void Update()
    {
        if (jumpAction.IsPressed())
        {
            //print("Jump!");

            if(manager.CurrMode == GameManager.Gamemode.Cube && isGrounded)
            {
                // Reset Y velocity for consistent jump height
                rb.linearVelocityY = 0;

                rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            }
            else if(manager.CurrMode == GameManager.Gamemode.Ship)
            {
                rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY + shipAccel * Time.deltaTime, -maxShipVel, maxShipVel);
            }

            isGrounded = false;
        }

        if (!isGrounded && manager.CurrMode == GameManager.Gamemode.Cube)
        {
            //In air cube rotation
            float newRot = visualsTransform.eulerAngles.z + rotationSpeed * Time.deltaTime; 
            visualsTransform.eulerAngles = Vector3.forward * newRot;
        }
        else if (manager.CurrMode == GameManager.Gamemode.Ship)
        {
            //Look in direction of travel
            visualsTransform.eulerAngles = rb.linearVelocityY * 3 * Vector3.forward;
            rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -maxShipVel, maxShipVel);
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
        //Check for spikes
        else if ((manager.spikesLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            manager.PlayerHitSpike();
            StartCoroutine(GameManager.Singleton.ResetLevelCoroutine(GameManager.resetLevelAfterDeathDelay));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((manager.specialLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Vector3Int[] checkOffsets = new Vector3Int[11]
            {
                Vector3Int.zero,
                Vector3Int.left,
                Vector3Int.right,
                Vector3Int.up,
                Vector3Int.down,
                Vector3Int.left + Vector3Int.down,
                Vector3Int.left + Vector3Int.up,
                Vector3Int.right + Vector3Int.down,
                Vector3Int.right + Vector3Int.up,
                new (-1, -2, 0),
                new(1, -2, 0)
            };

            foreach (Vector3Int offset in checkOffsets)
            {
                // Push the contact point slightly inward toward the tile to prevent rounding errors
                Vector3Int cellPosition = specialTiles.WorldToCell(transform.position) + offset;

                TileBase hitTile = specialTiles.GetTile(cellPosition);

                // 5. Check if it matches your special tile
                if (hitTile == coinTile && offset.y > -2)
                {
                    CoinManager.Instance.GotCoin();
                    specialTiles.SetTile(cellPosition, null); //Delete from tileMap
                }
                else if (hitTile == shipPortalTileTop || hitTile == shipPortalTileBottom)
                {
                    manager.SwitchGamemode(GameManager.Gamemode.Ship);
                }
                else if (hitTile == cubePortalTileTop || hitTile == cubePortalTileBottom)
                {
                    manager.SwitchGamemode(GameManager.Gamemode.Cube);
                }
            }
        }
    }
}
