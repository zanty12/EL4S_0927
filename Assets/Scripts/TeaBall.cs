using UnityEngine;

public class TeaBall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.color = ColorPicker.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
