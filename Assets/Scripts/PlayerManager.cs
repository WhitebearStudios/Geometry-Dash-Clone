using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerManager : MonoBehaviour
{
    public GameManager.Gamemode startGameMode = GameManager.Gamemode.Cube;

    [SerializeField] private PlayerMovement movement;

    [SerializeField] private Sprite cubeSprite, shipSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ParticleSystem playerKaboom, playerTrail;
    public GameManager.Gamemode CurrMode { get; private set; }

    const float cubeGravity = 11, shipGravity = 5;
    

    public LayerMask groundLayer, spikesLayer, specialLayer;



    //Called right when player hits spike
    public void PlayerHitSpike()
    {
        playerKaboom.Play();
        playerTrail.Stop();
        spriteRenderer.sprite = null;
        movement.boxCollider.enabled = false;
        movement.rb.simulated = false;
    }

    public void ResetPlayer()
    {
        SwitchGamemode(startGameMode);
        playerTrail.Play();
        movement.boxCollider.enabled = true;
        movement.rb.simulated = true;
    }

    public void SwitchGamemode(GameManager.Gamemode gamemode)
    {
        CurrMode = gamemode;

        if(gamemode == GameManager.Gamemode.Cube)
        {
            spriteRenderer.sprite = cubeSprite;
            movement.rb.gravityScale = cubeGravity;

        }
        else if (gamemode == GameManager.Gamemode.Ship)
        {
            spriteRenderer.sprite = shipSprite;
            movement.rb.gravityScale = shipGravity;
        }
    }
}
