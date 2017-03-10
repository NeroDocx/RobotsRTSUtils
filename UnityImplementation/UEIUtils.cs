// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;

namespace Utils.UnityImplementation
{
    public static class UEIUtils
    {

        #region QuatTransformVector
        /// <summary>
        /// Поворачивает vector на Quaternion
        /// </summary>
        /// <param name="_quat">Quaternion</param>
        /// <param name="_vector">исходный vector</param>
        /// <returns>новый vector</returns>
        public static UEIVector3 QuatTransformVector(UEIQuaternion _quat, UEIVector3 _vector)
        {
            UEIQuaternion t = QuatMulVector(_quat, _vector);
            t = QuatMulQuat(t, UEIQuaternion.Inverse(_quat));

            return new UEIVector3(t.X, t.Y, t.Z);
        }
        private static UEIQuaternion QuatMulVector(UEIQuaternion q, UEIVector3 v)
        {
            UEIQuaternion t = new UEIQuaternion(v.X, v.Y, v.Z, 0.0f);
            return QuatMulQuat(q, t);
        }

        private static UEIQuaternion QuatMulQuat(UEIQuaternion a, UEIQuaternion b)
        {
            return a * b;
        }
        #endregion

        #region Box
        /// <summary>
        /// Проверка на пересечение прямой box
        /// </summary>
        /// <param name="_box">проверяемый box</param>
        /// <param name="_origin">точка начала прямой</param>
        /// <param name="_dir">точка в направления прямой</param>
        /// <param name="_tnear"></param>
        /// <param name="_tfar"></param>
        /// <returns>результат</returns>
        public static bool AABBvsRayIntersect(ABBox _box, UEIVector3 _origin, UEIVector3 _dir, out float _tnear, out float _tfar)
        {
            UEIVector3 diff0 = _box.APoint - _origin;
            UEIVector3 diff1 = _box.BPoint - _origin;

            UEIVector3 t0 = new UEIVector3(diff0.X / _dir.X, diff0.Y / _dir.Y, diff0.Z / _dir.Z);
            UEIVector3 t1 = new UEIVector3(diff1.X / _dir.X, diff1.Y / _dir.Y, diff1.Z / _dir.Z);

            UEIVector3 tMin = new UEIVector3(Math.Min(t0.X, t1.X), Math.Min(t0.Y, t1.Y), Math.Min(t0.Z, t1.Z));
            UEIVector3 tMax = new UEIVector3(Math.Max(t0.X, t1.X), Math.Max(t0.Y, t1.Y), Math.Max(t0.Z, t1.Z));

            _tnear = Math.Max(Math.Max(tMin.X, tMin.Y), tMin.Z);
            _tfar = Math.Min(Math.Min(tMax.X, tMax.Y), tMax.Z);

            if (_tnear <= _tfar) return true;
            return false;
        }
        /// <summary>
        /// Проверка на пересечение прямой box
        /// </summary>
        /// <param name="_box">проверяемый box</param>
        /// <param name="_origin">точка начала прямой</param>
        /// <param name="_dir">точка в направления прямой</param>
        /// <returns>результат</returns>
        public static bool AABBvsRayIntersect(ABBox _box, UEIVector3 _origin, UEIVector3 _dir)
        {
            UEIVector3 diff0 = _box.APoint - _origin;
            UEIVector3 diff1 = _box.BPoint - _origin;

            UEIVector3 t0 = new UEIVector3(diff0.X / _dir.X, diff0.Y / _dir.Y, diff0.Z / _dir.Z);
            UEIVector3 t1 = new UEIVector3(diff1.X / _dir.X, diff1.Y / _dir.Y, diff1.Z / _dir.Z);

            UEIVector3 tMin = new UEIVector3(Math.Min(t0.X, t1.X), Math.Min(t0.Y, t1.Y), Math.Min(t0.Z, t1.Z));
            UEIVector3 tMax = new UEIVector3(Math.Max(t0.X, t1.X), Math.Max(t0.Y, t1.Y), Math.Max(t0.Z, t1.Z));

            float _tnear = Math.Max(Math.Max(tMin.X, tMin.Y), tMin.Z);
            float _tfar = Math.Min(Math.Min(tMax.X, tMax.Y), tMax.Z);

            if (_tnear <= _tfar) return true;
            return false;
        }
        /// <summary>
        /// Проверка на пересечение прямой box
        /// </summary>
        /// <param name="_box">проверяемый box</param>
        /// <param name="_origin">точка начала прямой</param>
        /// <param name="_dir">точка в направления прямой</param>
        /// <param name="_epsilon">_epsilon для проверки расстояния от центра _box до _origin</param>
        /// <returns>результат</returns>
        public static bool AABBvsRayIntersect(ABBox _box, UEIVector3 _origin, UEIVector3 _dir, float _epsilon)
        {
            bool LineIntersect = AABBvsRayIntersect(_box, _origin, _dir);
            float rayDistance = UEIVector3.Distance(_origin, _dir);
            float originToMidPointDistance = UEIVector3.Distance(_origin, UEIVector3.MidPoint(_box.APoint, _box.BPoint));
            return LineIntersect && (rayDistance - originToMidPointDistance) >= _epsilon;
        }
        #endregion
    }
}
