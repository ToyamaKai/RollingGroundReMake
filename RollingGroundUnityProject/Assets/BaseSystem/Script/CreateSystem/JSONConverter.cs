using UnityEngine;
using System.IO;

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

        string json = JsonUtility.ToJson(data, true);
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
