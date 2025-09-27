using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeightUI : MonoBehaviour
{
    //淹れた量を表示するUI
    [SerializeField] private TextMeshProUGUI heightText;
    private float heightOffset = 0.0f; // Offset to adjust the height display

    //淹れた量がtrophyHeightに達したら何か表示
    [SerializeField] private float trophyHeight = 1000.0f; // Height at which the trophy appears
    private float currentHeight = 0.0f; // Current height of the player
    [SerializeField]private float trophyDisplayDuration = 5.0f; // Duration to display the trophy message
    [SerializeField] private GameObject trophyMessage; // Reference to the trophy message UI element
    private TextMeshProUGUI trophyText;

    //溢した量のテキスト
    [SerializeField]private TextMeshProUGUI spillLossText;

    //tophyheightに達したときの音
    [SerializeField]private AudioSource trophySound; // Sound to play when trophy is reached
    [SerializeField]private AudioClip trophyClip;
    [SerializeField] private AudioClip secretClip;

    //注げた量とこぼした量を表示する
    [SerializeField] private TeaSpill teaSpill;//こぼした量を取得するため
    [SerializeField] private TeaReceive teaReceive;//注いだ量を取得するため



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
        if (!teaSpill)
        {
            teaSpill = FindAnyObjectByType<TeaSpill>();
        }
        if(!teaReceive)
        {
            teaReceive = FindAnyObjectByType<TeaReceive>();
        }
        heightOffset = 0.0f;
        currentHeight = trophyHeight;
        UiDraw();
    }


    //前回の高さからのオフセットを加算
    public void AddHeightOffset(float offset)
    {
        heightOffset += offset;
        CheckTorophyHeight();
        UiDraw();
    }

    private void CheckTorophyHeight()
    {
        if (currentHeight < heightOffset)
        {
            currentHeight += trophyHeight;
            OnTrophyDraw();
        }
        if (heightOffset > 99999.0f)
        {
            heightOffset = 0.0f;
            trophySound?.PlayOneShot(secretClip);
        }
    }

    int _lastReceivedTeaCount = 0;
    int _lastSpillLossCount = 0;
    // Update is called once per frame
    void Update()
    {
       if(_lastReceivedTeaCount != teaReceive.ReceivedTeaCount)
        {
            heightOffset += (teaReceive.ReceivedTeaCount - _lastReceivedTeaCount)*0.1f;
            _lastReceivedTeaCount = teaReceive.ReceivedTeaCount;
            CheckTorophyHeight();
            UiDraw();
        }
        if (_lastSpillLossCount != teaSpill.spillCount)
        {
            _lastSpillLossCount = teaSpill.spillCount;
            CheckTorophyHeight();
            UiDraw();
        }
    }

    private void UiDraw()
    {
        if (heightText != null)
        {
            heightText.text = heightOffset.ToString("00000") + " L";
        }
        if (spillLossText != null)
        {
            spillLossText.text = "-$" + teaSpill.spillCount.ToString("000") + "0000";
        }
    }

    private void OnTrophyDraw()
    {
        trophySound?.PlayOneShot(trophyClip);
        trophyMessage.SetActive(true);
        trophyText.text = "Congratulations!\n You've reached\n" + currentHeight.ToString("00000") + " L!";
        Invoke("OffTrophyDraw", trophyDisplayDuration);
    }
    private void OffTrophyDraw()
    {
        trophyMessage.SetActive(false);
    }
}
