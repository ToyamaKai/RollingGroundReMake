using System;
using System.Collections.Generic;

/// <summary>
/// ステージのデータクラス
/// </summary>
[Serializable]
public class StageData
{
    public StageMetaData StageMetaData;
    public StageSizeData StageSize;
    public StageSettingData StageSetting;
    public List<BlockSaveData> Blocks = new();
    public List<CharaData> Charas = new();
}