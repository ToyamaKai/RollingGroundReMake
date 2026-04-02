using UnityEngine;
using SN = System.Numerics;

namespace RollingGround
{
    public static class NumericExtensions
    {
        public static SN.Vector3 ToSystem(this Vector3 v) => new SN.Vector3(v.x, v.y, v.z);
        public static SN.Quaternion ToSystem(this Quaternion q) => new SN.Quaternion(q.x, q.y, q.z, q.w);

        public static Vector3 ToUnity(this SN.Vector3 v) => new Vector3(v.X, v.Y, v.Z);
    }
}
