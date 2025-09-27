using UnityEngine;

public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] private Material gameSceneMetaBallMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSceneMetaBallMaterial.color = ColorPicker.color;
        gameSceneMetaBallMaterial.SetColor("_StrokeColor", ColorPicker.strokeColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
