using UnityEngine;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// JSON変換スクリプト
/// </summary>
public class JSONConverter : MonoBehaviour
{
    StageExporter stageExporter = new StageExporter();
    MStageBuilder m_stageBuilder = new MStageBuilder();
    StageBlockManager stageBlockManager;
    string path = Path.Combine(Application.dataPath, "StageData.json");

    void Awake()
    {
        stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
        m_stageBuilder = GameObject.FindFirstObjectByType<MStageBuilder>();
    }

    public void StageJSONConvert()
    {
        StageData data = stageExporter.Export(stageBlockManager.GetBlockTypeMap());

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void StageJsonDeserialize()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StageData data = JsonConvert.DeserializeObject<StageData>(json);
            // ここでdataを元にステージを再構築する処理を実装
            m_stageBuilder.BuildStage(data);
        }
        else
        {
            Debug.LogError("StageData.jsonが見つかりません。");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StageJSONConvert();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            StageJsonDeserialize();
        }
    }
}
