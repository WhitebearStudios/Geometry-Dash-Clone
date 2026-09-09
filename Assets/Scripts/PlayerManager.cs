using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameManager.Gamemode startGameMode = GameManager.Gamemode.Cube;

    [SerializeField] private Sprite cubeSprite, shipSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ParticleSystem playerKaboom, playerTrail;
    public GameManager.Gamemode CurrMode { get; private set; }

    

    public LayerMask groundLayer, spikesLayer;
    
    //Called right when player hits spike
    public void PlayerHitSpike()
    {
        playerKaboom.Play();
        playerTrail.Stop();
        spriteRenderer.sprite = null;
    }

    public void ResetPlayer()
    {
        SwitchGamemode(startGameMode);
        playerTrail.Play();
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
