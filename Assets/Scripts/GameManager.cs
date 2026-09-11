using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum Gamemode
    {
        Cube,
        Ship
    }

    public static GameManager Singleton { get; private set; }

    public static readonly Vector3 playerStartPos = new(-4.5144f, -3.071576f, 0);
    public static readonly Vector3 levelStartPos = new(0, -0.58f, 0);
    public float levelEnd => Mathf.Max(RightXBound(groundTiles.localBounds), Mathf.Max(RightXBound(spikeTiles.localBounds), RightXBound(specialTiles.localBounds))) + 15;
    public const float resetLevelAfterDeathDelay = 2f;
    public float levelSpeedMultiplier = 1;
    public float levelBaseSpeed = 30;

    [SerializeField] private PlayerManager player;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform levelTransform;
    [SerializeField] private Transform levelEndTransform;
    [SerializeField] private GameObject levelCompleteText;
    public Tilemap groundTiles, spikeTiles, specialTiles;

    private bool advanceLevel = true;

    private void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        ResetLevel();
    }

    private void LateUpdate()
    {
        if(advanceLevel)
        {
            if (camera.ViewportToWorldPoint(Vector3.right).x < levelEnd + levelTransform.position.x) levelTransform.position += levelBaseSpeed * levelSpeedMultiplier * Time.deltaTime * Vector3.left;
            else levelCompleteText.SetActive(true);
        }
    }

    void ResetLevel()
    {
        player.ResetPlayer();
        player.transform.position = playerStartPos;
        levelTransform.position = levelStartPos;

        advanceLevel = true;
        AudioManager.instance.RestartMusic();

        levelEndTransform.position = Vector3.right * levelEnd;
    }

    float RightXBound(Bounds bounds) => bounds.center.x + bounds.extents.x;

    public IEnumerator ResetLevelCoroutine(float delay)
    {
        advanceLevel = false;
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFX(AudioManager.instance.dieSFX);

        yield return new WaitForSeconds(delay);

        ResetLevel();
    }
}
