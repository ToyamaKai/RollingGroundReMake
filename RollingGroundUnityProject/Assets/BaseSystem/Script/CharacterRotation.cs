namespace RollingGround.Logic
{
    public enum CharacterRotationAxis { X, Y, Z };

    /// <summary>
    /// キャラ回転の実処理
    /// </summary>
    public static class CharacterRotationLogic
    {
        public static void ExecuteCharacterRotate(CharacterRotationAxis axis, int direction)
        {

        }

        public static void ExecutePlayerRotate(PlayerData data, CharacterRotationAxis axis, int direction)
        {
            switch (axis)
            {
                case CharacterRotationAxis.X: data.WorldStepX = ClampStep(data.WorldStepX + direction); break;
                case CharacterRotationAxis.Y: data.WorldStepY = ClampStep(data.WorldStepY + direction); break;
                case CharacterRotationAxis.Z: data.WorldStepZ = ClampStep(data.WorldStepZ + direction); break;
            }
        }

        private static int ClampStep(int step)
        {
            return (step % 4 + 4) % 4;
        }
    }
}