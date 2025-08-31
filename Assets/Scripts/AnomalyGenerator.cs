using UnityEngine;

using System.Collections.Generic;
using System.Linq;

public class AnomalyGenerator : MonoBehaviour
{
    // 企画書に基づいた確率設定
    private const float CHANCE_FOR_NORMAL_ANOMALY = 0.5f;
    private const float CHANCE_FOR_RARE_ANOMALY = 0.0005f;

    // 階層が上がるにつれて激しい異変の確率を上げる
    public float intenseAnomalyChanceModifier = 0.05f;

    public enum AnomalyType
    {
        None,
        Normal,
        Intense,
        Rare
    }

    // 現在の階の異変状態
    public AnomalyType currentAnomaly { get; private set; } = AnomalyType.None;

    private List<IAnomaly> allAnomalies;
    private IAnomaly activeAnomaly = null;

    private void Awake()
    {
        // シーン内の全てのIAnomalyを検索してリストに格納
        allAnomalies = FindObjectsOfType<MonoBehaviour>().OfType<IAnomaly>().ToList();
        // 最初は全ての異変を非アクティブ化
        foreach (var anomaly in allAnomalies)
        {
            anomaly.Deactivate();
        }
    }

    // 新しい階のために異変を生成する
    public void GenerateAnomalyForFloor(int floor)
    {
        // 前の階の異変をリセット
        if (activeAnomaly != null)
        {
            activeAnomaly.Deactivate();
            activeAnomaly = null;
        }
        currentAnomaly = AnomalyType.None;

        // 確率計算
        float intenseChance = Mathf.Max(0, floor) * intenseAnomalyChanceModifier; // 地下は0として計算
        float randomValue = Random.value;

        if (randomValue < CHANCE_FOR_RARE_ANOMALY)
        {
            currentAnomaly = AnomalyType.Rare;
        }
        else if (randomValue < intenseChance)
        {
            currentAnomaly = AnomalyType.Intense;
        }
        else if (randomValue < CHANCE_FOR_NORMAL_ANOMALY)
        {
            currentAnomaly = AnomalyType.Normal;
        }

        // 異変を発生させる
        if (currentAnomaly != AnomalyType.None)
        {
            if (allAnomalies.Count > 0)
            {
                // 利用可能な異変の中からランダムに一つ選んで有効化
                activeAnomaly = allAnomalies[Random.Range(0, allAnomalies.Count)];
                activeAnomaly.Activate();
                Debug.Log("階層 " + floor + ": " + currentAnomaly + " 異変 (" + activeAnomaly.GetType().Name + ") を生成しました。");
            }
            else
            {
                Debug.LogWarning("異変を生成しようとしましたが、利用可能なIAnomalyコンポーネントがシーンにありません。");
            }
        }
        else
        {
            Debug.Log("階層 " + floor + ": 異変はありません。");
        }
    }
}
