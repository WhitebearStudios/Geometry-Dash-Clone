using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerManager : MonoBehaviour
{
    public GameManager.Gamemode startGameMode = GameManager.Gamemode.Cube;

    [SerializeField] private Sprite cubeSprite, shipSprite;
    private SpriteRenderer spriteRenderer;

    private InputAction jumpAction;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        jumpAction = InputSystem.actions.FindAction("Jump");

        SwitchGamemode(startGameMode);
    }

    private void Update()
    {
        if (jumpAction.IsPressed()) print("Jump!");
    }

    public void SwitchGamemode(GameManager.Gamemode gamemode)
    {
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
