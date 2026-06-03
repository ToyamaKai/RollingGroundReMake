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
    string path = Path.Combine(Application.dataPath, "StageData.json");

    void Awake()
    {
        stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
    }

    public void StageJSONConvert()
    {
        StageData data = stageExporter.Export(stageBlockManager.GetBlockTypeMap());

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StageJSONConvert();
        }
    }
}
