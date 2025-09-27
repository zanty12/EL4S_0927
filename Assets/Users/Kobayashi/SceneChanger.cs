using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Text text;

    private string sceneToLoad;

#if UNITY_EDITOR
    // インスペクタに表示するためのSceneAsset型変数
    [Header("遷移先シーン選択")] // インスペクタに見出しを表示
    [SerializeField] private SceneAsset sceneAsset; // ここにシーンファイルをD&Dする
#endif

    private void Start()
    {
        text.text = "";
    }

    public void OnClickLoadScene()
    {
        if(ColorPicker.Hue >= 0.193 && ColorPicker.Hue <= 0.393)
        {
            text.color = Color.red;
            text.text = "NO Japonism !!!";
            return;
        }
        else
        {
            text.text = "";
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneChange();
        }
        else
        {
            Debug.LogError("遷移先のシーン名が設定されていません！");
        }
    }


    public void SceneChange()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // OnValidateメソッドもエディタ専用
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneToLoad = sceneAsset.name;
        }
        else
        {
            sceneToLoad = "";
        }
    }
#endif
}
