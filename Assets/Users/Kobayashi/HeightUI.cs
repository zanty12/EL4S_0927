using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeightUI : MonoBehaviour
{
    //進んだ距離を表示するUI
    [SerializeField] private TextMeshProUGUI heightText;
    private float heightOffset = 0.0f; // Offset to adjust the height display

    //進んだ距離がtrophyHeightに達したら何か表示
    [SerializeField] private float trophyHeight = 1000.0f; // Height at which the trophy appears
    private float currentHeight = 0.0f; // Current height of the player
    [SerializeField]private float trophyDisplayDuration = 5.0f; // Duration to display the trophy message
    [SerializeField] private GameObject trophyMessage; // Reference to the trophy message UI element
    private TextMeshProUGUI trophyText;
    [SerializeField]private AudioSource trophySound; // Sound to play when trophy is reached
    [SerializeField]private AudioClip trophyClip;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (heightText == null)
        {
            heightText = GetComponent<TextMeshProUGUI>();
        }
        if (trophyMessage != null)
        {
            trophyText = trophyMessage.GetComponent<TextMeshProUGUI>();
            trophyMessage.SetActive(false);
        }
        if (trophySound)
        {
            trophySound.clip = trophyClip;
        }
        heightOffset = 0.0f;
        currentHeight = trophyHeight;
        UiDraw();
    }

    public void AddHeightOffset(float offset)
    {
        heightOffset += offset;
        if (currentHeight < heightOffset)
        {
            currentHeight += trophyHeight;
            OnTrophyDraw();
        }
        UiDraw();
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHeightOffset(100.0f); // Increase height offset by 10 meters
        }
    }
#endif

    private void UiDraw()
    {
        if (heightText != null)
        {
            heightText.text = heightOffset.ToString("00000") + " m";
        }
    }

    private void OnTrophyDraw()
    {
        trophySound?.PlayOneShot(trophyClip);
        trophyMessage.SetActive(true);
        trophyText.text = "Congratulations!\n You've reached\n" + currentHeight.ToString("00000") + " m!";
        Invoke("OffTrophyDraw", trophyDisplayDuration);
    }
    private void OffTrophyDraw()
    {
        trophyMessage.SetActive(false);
    }
}
