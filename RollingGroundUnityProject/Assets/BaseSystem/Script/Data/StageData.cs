namespace RollingGround
{
    /// <summary>
    /// ステージ情報を保持するデータクラス
    /// </summary>
    public class StageData
    {
        //0: 0度, 1: 90度, 2: 180度, 3: 270度 という設計 (4になったら0に戻るようにLogic側で制御する)
        public int StepX { get; set; }
        public int StepY { get; set; }
        public int StepZ { get; set; }

        public StageData(int stepX = 0, int stepY = 0, int stepZ = 0)
        {
            StepX = stepX;
            StepY = stepY;
            StepZ = stepZ;
        }
    }
}