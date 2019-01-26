using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DecreaseHealth()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r/2f, spriteRenderer.color.g/2f, spriteRenderer.color.b/2f);
    }
}
