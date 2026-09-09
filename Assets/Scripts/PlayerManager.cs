using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameManager.Gamemode startGameMode = GameManager.Gamemode.Cube;

    [SerializeField] private Sprite cubeSprite, shipSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public GameManager.Gamemode CurrMode { get; private set; }

    

    public LayerMask groundLayer, spikesLayer;
    

    void Awake()
    {
        SwitchGamemode(startGameMode);
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
