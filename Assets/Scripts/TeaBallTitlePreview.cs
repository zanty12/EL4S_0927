using UnityEngine;

public class TeaBallTitlePreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 min;
    private Vector2 max;

    private Vector2 dir;
    private float speed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.color = ColorPicker.color;

        min = new Vector2(1570, 1200);
        max = new Vector2(1590, 1230);

        speed = UnityEngine.Random.Range(0.01f, 0.08f);
        dir.x = UnityEngine.Random.Range(-1f, 1f);
        dir.y = UnityEngine.Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = ColorPicker.color;

        Vector2 pos = transform.position;
        pos += dir * speed;

        if(pos.x < min.x)
        {
            float sub = min.x - pos.x;
            pos.x += sub;
            dir.x *= -1f;
        }
        else if(pos.x > max.x)
        {
            float sub = max.x - pos.x;
            pos.x += sub;
            dir.x *= -1f;
        }

        if(pos.y < min.y)
        {
            float sub = min.y - pos.y;
            pos.y += sub;
            dir.y *= -1f;
        }
        else if(pos.y > max.y)
        {
            float sub = max.y - pos.y;
            pos.y += sub;
            dir.y *= -1f;
        }

        transform.localPosition = pos;
    }
}
