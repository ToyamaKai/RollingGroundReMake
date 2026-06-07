using UnityEngine;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// JSON変換スクリプト
/// </summary>
public class JSONConverter : MonoBehaviour
{
    StageExporter stageExporter = new StageExporter();
    StageBlockManager stageBlockManager;
    MStageBuilder m_stageBuilder;

    string path = Path.Combine(Application.dataPath, "StageData.json"); // JSONファイルの保存先パス

    void Awake()
    {
        stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
        m_stageBuilder = GameObject.FindFirstObjectByType<MStageBuilder>();
        Debug.Log(path);
    }

    /// <summary>
    /// ステージのブロックデータをJSON形式に変換して保存するメソッド
    /// </summary>
    public void StageJSONConvert()
    {
        StageData data = stageExporter.Export(stageBlockManager.GetBlockTypeMap());
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// JSONファイルからステージデータを読み込み、ステージを再構築するメソッド
    /// </summary>
    /// <param name="dataPath">ステージデータのパス</param>
    public void StageJsonDeserialize(string dataPath)
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            StageData data = JsonConvert.DeserializeObject<StageData>(json);
            m_stageBuilder.BuildStage(data);
        }
        else
        {
            Debug.LogError("StageData.jsonが見つかりません。");
        }
    }
}
