namespace RollingGround.Logic
{
    /// <summary>
    /// プレイヤー情報を保持するデータクラス
    /// </summary>
    public class PlayerData
    {
        //0: 0度, 1: 90度, 2: 180度, 3: 270度 という設計 (4になったら0に戻るようにLogic側で制御する)
        public int WorldStepX { get; set; }
        public int WorldStepY { get; set; }
        public int WorldStepZ { get; set; }

        public PlayerData(int stepX = 0, int stepY = 0, int stepZ = 0)
        {
            WorldStepX = stepX;
            WorldStepY = stepY;
            WorldStepZ = stepZ;
        }
    }
}
