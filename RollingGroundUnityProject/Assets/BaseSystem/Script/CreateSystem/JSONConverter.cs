using UnityEngine;

/// <summary>
/// JSON変換スクリプト
/// </summary>
public class JSONConverter : MonoBehaviour
{
    StageExporter stageExporter = new StageExporter();
    StageBlockManager stageBlockManager;

    void Awake()
    {
        stageBlockManager = GameObject.FindFirstObjectByType<StageBlockManager>();
    }

    public void StageJSONConvert()
    {
        StageData data = stageExporter.Export(stageBlockManager.GetBlockTypeMap());

        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StageJSONConvert();
        }
    }
}
