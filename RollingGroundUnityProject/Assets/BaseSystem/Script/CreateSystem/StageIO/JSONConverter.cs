using UnityEngine;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// JSON変換スクリプト
/// </summary>
public class JSONConverter
{
    StageExporter m_stageExporter;
    StageBlockManager m_stageBlockManager; // Monobehaviourです
    MStageBuilder m_stageBuilder;
    MStageBlockCleaner m_stageBlockCleaner;

    string path = Path.Combine(Application.dataPath, "StageData.json"); // JSONファイルの保存先パス

    public JSONConverter(StageExporter stageExporter, StageBlockManager stageBlockManager, MStageBuilder stageBuilder, MStageBlockCleaner stageBlockCleaner)
    {
        m_stageExporter = stageExporter;
        m_stageBlockManager = stageBlockManager;
        m_stageBuilder = stageBuilder;
        m_stageBlockCleaner = stageBlockCleaner;
    }

    /// <summary>
    /// ステージのブロックデータをJSON形式に変換して保存するメソッド
    /// </summary>
    public void StageJSONConvert()
    {
        StageData data = m_stageExporter.Export(m_stageBlockManager.GetBlockTypeMap());
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// JSONファイルからステージデータを読み込み、ステージを再構築するメソッド
    /// </summary>
    /// <param name="dataPath">ステージデータのパス</param>
    public void StageJsonDeserialize(string dataPath)
    {
        if (File.Exists(path))
        {
            m_stageBlockCleaner.CleanStageObject();
            //string json = File.ReadAllText(dataPath);
            string json = File.ReadAllText(path);
            StageData data = JsonConvert.DeserializeObject<StageData>(json);
            m_stageBuilder.BuildStage(data);
        }
        else
        {
            Debug.LogError("StageData.jsonが見つかりません。");
        }
    }
}
