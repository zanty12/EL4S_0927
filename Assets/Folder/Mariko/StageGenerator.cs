using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockTypes
{
    public GameObject _prefab;
    [Range(0.0f, 256.0f)] public float _ratio = 1.0f;
}

public class StageGenerator : MonoBehaviour
{
    [Header("生成するブロックのPrefab")]
    [SerializeField, Tooltip("リスト")] private List<BlockTypes> _blockList = new List<BlockTypes>();

    [Header("ブロックをまとめる親オブジェクト")]
    [SerializeField] private Transform _stageParent;

    [Header("ブロックの分割数")]
    [SerializeField, Range(2, 128)] private int _NumberOfBlockDivisionRows = 16;
    [SerializeField, Range(2, 128)] private int _NumberOfBlockDivisionCols = 16;

    [Header("ブロックの分割数")]
    [SerializeField] private Vector2 _screenSize = new Vector2(5.7f, 10.0f);

    // 仮のステージデータ
    private int[,] stageData =
    {
        {0, 1, 1, 1, 0},
        {1, 0, 2, 0, 1},
        {1, 2, 2, 2, 1},
        {1, 0, 2, 0, 1},
        {0, 1, 1, 1, 0},
    };

    void Awake()
    {

        int[,] mapData = stageData;

        // ランダム生成
        mapData = RandomCreateMap();

        // ステージ生成
        GenerateStage(mapData);
    }

    // ランダムマップ生成
    private int[,] RandomCreateMap()
    {
        int rows = _NumberOfBlockDivisionRows;
        int cols = _NumberOfBlockDivisionCols;
        int[,] mapData = new int[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                mapData[y, x] = GetRandomValue();
            }
        }

        Debug.Log(mapData);

        return mapData;
    }

    private int GetRandomValue()
    {
        float totalWeight = 0.0f;

        // 割合を総計算
        foreach (var blockType in _blockList)
        {
            totalWeight += blockType._ratio;
        }
        // 乱数を生成（UnityEngine.Randomを明示）
        float rand = UnityEngine.Random.Range(0f, totalWeight);

        // 閾値判定
        float sum = 0f;
        foreach (var blockType in _blockList)
        {
            sum += blockType._ratio;
            if (rand < sum)
                return _blockList.IndexOf(blockType);
        }

        // 万が一、端数の計算誤差で到達した場合は最後の値を返す
        return _blockList.IndexOf(_blockList[_blockList.Count - 1]);
    }

    /// <summary>
    /// 2次元配列からステージを生成する
    /// </summary>
    /// <param name="data">ステージデータ</param>
    private void GenerateStage(int[,] data)
    {
        int rows = data.GetLength(0);
        int cols = data.GetLength(1);
        float cellSize = _screenSize.x / rows;
        Vector2 positionParent = new Vector2(_stageParent.position.x + cellSize / 2.0f, _stageParent.position.y - cellSize / 2.0f);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // プレハブがNoneであればコンティニューをする
                if (_blockList[data[y, x]]._prefab == null) continue;

                Vector3 spawnPos = new Vector3(x * cellSize + positionParent.x, -y * cellSize + positionParent.y, 0);
                // ※2Dゲームなら (x * cellSize, y * cellSize, 0) にする

                GameObject floor = Instantiate(_blockList[data[y, x]]._prefab, spawnPos, Quaternion.identity, _stageParent);
                floor.transform.localScale = new Vector3(cellSize, cellSize, 0.0f);
            }
        }
    }
}