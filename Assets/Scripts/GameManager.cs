using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Gamemode
    {
        Cube,
        Ship
    }

    public static GameManager Singleton { get; private set; }

    public static readonly Vector3 playerStartPos = new(-4.5144f, -2.0091f, 0);
    public static readonly Vector3 levelStartPos = Vector3.zero;
    public const float resetLevelAfterDeathDelay = 2f;
    public float levelSpeed = 1;

    [SerializeField] private PlayerManager player;
    [SerializeField] private Transform levelTransform;

    private bool advanceLevel = true;

    private void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        ResetLevel();
    }

    private void Update()
    {
        if(advanceLevel) levelTransform.position += levelSpeed * Time.deltaTime * Vector3.left;
    }

    void ResetLevel()
    {
        player.ResetPlayer();
        levelTransform.position = levelStartPos;

        advanceLevel = true;
    }

    public IEnumerator ResetLevelCoroutine(float delay)
    {
        advanceLevel = false;

        yield return new WaitForSeconds(delay);

        ResetLevel();
    }
}
