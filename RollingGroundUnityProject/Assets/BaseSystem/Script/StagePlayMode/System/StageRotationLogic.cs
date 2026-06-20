namespace RollingGround.Logic
{
    public enum StageRotationAxis { X, Y, Z }; //回転方向を指定する際に使用

    /// <summary>
    /// ステージ回転の実処理
    /// </summary>
    public static class StageRotationLogic
    {
        /// <summary>
        /// 入力を受け取り、StageRotationStateの回転値を0~3の間で更新するロジック
        /// </summary>
        /// <param name="data"></param>
        /// <param name="axis"></param>
        /// <param name="direction"></param>
        public static void ExecuteRotate(StageRotationState data, StageRotationAxis axis, int direction)
        {
            switch (axis)
            {
                case StageRotationAxis.X: data.StepX = ClamStep(data.StepX + direction); break;
                case StageRotationAxis.Y: data.StepY = ClamStep(data.StepY + direction); break;
                case StageRotationAxis.Z: data.StepZ = ClamStep(data.StepZ + direction); break;
            }
        }

        /// <summary>
        ///  0~3の範囲に丸め込む
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private static int ClamStep(int step)
        {
            return (step % 4 + 4) % 4;
        }
    }
}
