using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPos;
    private Vector2 currentOffset;
    public float scrollScale = 1;

    void Start()
    {
        // Automatically find the main camera if not assigned
        if (parentTransform == null)
        {
            parentTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Check how far the parent has moved
        float diff = parentTransform.position.x - lastPos.x;
        currentOffset.x += diff * scrollScale;

        print(currentOffset.x);

        spriteRenderer.material.SetVector("_ScrollOffset", currentOffset);

        lastPos = parentTransform.position;
    }
}
