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
        // Warn if parent not assigned
        if (parentTransform == null)
        {
            Debug.LogWarning("Parent transform for scroller script on " + gameObject.name + " isn't assgined!");
        }
    }

    void Update()
    {
        if (parentTransform == null) return;
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Check how far the parent has moved
        float diff = parentTransform.position.x - lastPos.x;
        //Change our offset
        currentOffset.x -= diff * scrollScale;

        //print(currentOffset.x);

        //Tell the shader graph how much to move the texture
        spriteRenderer.material.SetVector("_ScrollOffset", currentOffset);

        //Remember the last parent position
        lastPos = parentTransform.position;
    }
}
