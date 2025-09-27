using UnityEngine;

public class ParticleEmitAtMouse : MonoBehaviour
{
    [SerializeField] private Camera _cam; // カメラ
    [SerializeField] private float _objectZ = 0f; // パーティクルを置きたいワールドZ座標

    void Start()
    {
        if (_cam == null)
            _cam = Camera.main;
    }

    void Update()
    {
        // マウス位置を取得
        Vector3 mousePos = Input.mousePosition;

        // ▼重要！カメラからオブジェクトまでの距離を設定
        mousePos.z = Mathf.Abs(_cam.transform.position.z - _objectZ);

        // スクリーン座標 → ワールド座標に変換
        Vector3 worldPos = _cam.ScreenToWorldPoint(mousePos);

        // パーティクルをマウス位置に移動
        transform.position = worldPos;
    }
}
