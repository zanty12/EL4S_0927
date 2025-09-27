using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Material titleMetaBallPreview;
    [SerializeField] private Color defaultStrokeColor = Color.white;

    public Color defaultColor = Color.white;
    public Slider hueSlider;
    public Slider saturationSlider;
    public Slider valueSlider;
    public Image hueSliderBackground;
    public Image saturationSliderBackground;
    public Image valueSliderBackground;
    private const int GradientTextureWidth = 256; // グラデーションテクスチャの幅

    public static Color color = Color.white;
    public static Color strokeColor = Color.white;
    public static float Hue;

    void Start()
    {
        // スライダーの背景テクスチャを更新
        UpdateHueSliderBackground();
        UpdateSaturationValueSliderBackgrounds(hueSlider.value);

        hueSlider.onValueChanged.AddListener(delegate { OnHueValueChanged(hueSlider.value); });
        saturationSlider.onValueChanged.AddListener(delegate { SaveColor(); });
        valueSlider.onValueChanged.AddListener(delegate { SaveColor(); });

        Color.RGBToHSV(defaultColor, out float h, out float s, out float v);
        hueSlider.value = h;
        saturationSlider.value = s;
        valueSlider.value = v;
        Hue = h;
        SaveColor();
    }

    void OnHueValueChanged(float hue)
    {
        UpdateSaturationValueSliderBackgrounds(hue);
        Hue = hue;
        SaveColor();
    }


    public void SaveColor()
    {
        Color edgeColorSub = defaultStrokeColor - defaultColor;

        Color newColor = Color.HSVToRGB(hueSlider.value, saturationSlider.value, valueSlider.value);

        color = newColor;
        strokeColor = newColor + edgeColorSub;

        titleMetaBallPreview.color = newColor;
        titleMetaBallPreview.SetColor("_StrokeColor", newColor + edgeColorSub);
    }
    
    void UpdateHueSliderBackground()
    {
        hueSliderBackground.sprite = Sprite.Create(CreateHueGradient(), new Rect(0, 0, GradientTextureWidth, 1), new Vector2(0.5f, 0.5f));
    }

    void UpdateSaturationValueSliderBackgrounds(float hue)
    {
        saturationSliderBackground.sprite = Sprite.Create(CreateSaturationGradient(hue), new Rect(0, 0, GradientTextureWidth, 1), new Vector2(0.5f, 0.5f));
        valueSliderBackground.sprite = Sprite.Create(CreateValueGradient(hue), new Rect(0, 0, GradientTextureWidth, 1), new Vector2(0.5f, 0.5f));
    }

    Texture2D CreateHueGradient()
    {
        Texture2D texture = new Texture2D(GradientTextureWidth, 1, TextureFormat.RGB24, false);
        for (int i = 0; i < GradientTextureWidth; i++)
        {
            Color color = Color.HSVToRGB(i / (float)GradientTextureWidth, 1f, 1f);
            texture.SetPixel(i, 0, color);
        }
        texture.Apply();
        return texture;
    }

    Texture2D CreateSaturationGradient(float hue)
    {
        Texture2D texture = new Texture2D(GradientTextureWidth, 1, TextureFormat.RGB24, false);
        for (int i = 0; i < GradientTextureWidth; i++)
        {
            Color color = Color.HSVToRGB(hue, i / (float)GradientTextureWidth, 1f);
            texture.SetPixel(i, 0, color);
        }
        texture.Apply();
        return texture;
    }

    Texture2D CreateValueGradient(float hue)
    {
        Texture2D texture = new Texture2D(GradientTextureWidth, 1, TextureFormat.RGB24, false);
        for (int i = 0; i < GradientTextureWidth; i++)
        {
            Color color = Color.HSVToRGB(hue, 1f, i / (float)GradientTextureWidth);
            texture.SetPixel(i, 0, color);
        }
        texture.Apply();
        return texture;
    }
}