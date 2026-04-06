namespace RollingGround.Logic
{
    public enum CharacterRotationAxis { X, Y, Z };

    /// <summary>
    /// キャラ回転の実処理
    /// </summary>
    public static class CharacterRotationLogic
    {
        /// <summary>
        /// キャラの回転状況を更新する処理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="axis"></param>
        /// <param name="direction"></param>
        public static void ExecuteCharacterRotate(CharaData data, CharacterRotationAxis axis, int direction)
        {
            switch (axis)
            {
                case CharacterRotationAxis.X: data.StepX = ClampStep(data.StepX + direction); break;
                case CharacterRotationAxis.Y: data.StepY = ClampStep(data.StepY + direction); break;
                case CharacterRotationAxis.Z: data.StepZ = ClampStep(data.StepZ + direction); break;
            }
        }

        /// <summary>
        /// プレイヤーの回転状況を更新する処理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="axis"></param>
        /// <param name="direction"></param>
        public static void ExecutePlayerRotate(PlayerData data, CharacterRotationAxis axis, int direction)
        {
            switch (axis)
            {
                case CharacterRotationAxis.X: data.StepX = ClampStep(data.StepX + direction); break;
                case CharacterRotationAxis.Y: data.StepY = ClampStep(data.StepY + direction); break;
                case CharacterRotationAxis.Z: data.StepZ = ClampStep(data.StepZ + direction); break;
            }
        }

        private static int ClampStep(int step)
        {
            return (step % 4 + 4) % 4;
        }
    }
}