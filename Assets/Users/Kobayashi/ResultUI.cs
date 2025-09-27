using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI resultTeaSpillText;//こぼした損害を表示する
    [SerializeField] private TextMeshProUGUI resultTeaReceiveText;//注いだ量を表示する

    public string teaSpillKey = "TeaSpill";
    public string teaReceiveKey = "TeaReceive";

    void Awake()
    {
        if (resultTeaSpillText == null)
        {
            resultTeaSpillText = GetComponent<TextMeshProUGUI>();
        }
        if (resultTeaReceiveText == null)
        {
            resultTeaReceiveText = GetComponent<TextMeshProUGUI>();
        }
        resultTeaReceiveText.text = PlayerPrefs.GetFloat(teaReceiveKey, 0.0f).ToString("00000") + " oz";
        resultTeaSpillText.text = "-£" + PlayerPrefs.GetFloat(teaSpillKey, 0.0f).ToString("000") + ",000,000";
    }
}
