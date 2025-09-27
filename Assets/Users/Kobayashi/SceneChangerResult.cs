using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangerResult : MonoBehaviour
{
    private string sceneToLoad;

#if UNITY_EDITOR
    // インスペクタに表示するためのSceneAsset型変数
    [Header("遷移先シーン選択")] // インスペクタに見出しを表示
    [SerializeField] private SceneAsset sceneAsset; // ここにシーンファイルをD&Dする
#endif

    private void Start()
    {
    }

    public void OnClickLoadScene()
    {

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
