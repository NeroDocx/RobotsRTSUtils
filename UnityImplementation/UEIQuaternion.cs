// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
namespace Utils.UnityImplementation
{
    /// <summary>
    /// Структура кватеринион. Четырехкомпонентный вектор XYZW
    /// </summary>
    public struct UEIQuaternion
    {
        #region Values
        const float radToDeg = (float)(180.0f / Math.PI);
        const float degToRad = (float)(Math.PI / 180.0f);
        const float Rad2Deg = 57.29578f;


        public const float kEpsilon = 1E-05F;
        /// <summary>
        /// X компонент UEIQuaternion
        /// </summary>
        public float X { get; private set; }
        /// <summary>
        /// Y компонент UEIQuaternion
        /// </summary>
        public float Y { get; private set; }
        /// <summary>
        /// Z компонент UEIQuaternion
        /// </summary>
        public float Z { get; private set; }
        /// <summary>
        /// W компонент UEIQuaternion
        /// </summary>
        public float W { get; private set; }
        public UEIVector3 XYZ
        {
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
            get
            {
                return new UEIVector3(X, Y, Z);
            }
        }
        //
        // Summary:
        //     ///
        //     The identity rotation (Read Only).
        //     ///
        public static UEIQuaternion identity { get { return new UEIQuaternion(0f, 0f, 0f, 1f); } }
        //
        // Summary:
        //     ///
        //     Returns the euler angle representation of the rotation.
        //     ///
        public UEIVector3 eulerAngles
        {
            get
            {
                return (UEIVector3)(Internal_ToEulerRad(this) * radToDeg);
            }
            set
            {
                this = Internal_FromEulerRad((UEIVector3)(value * degToRad));
            }
        }
        #endregion
        /// <summary>
        /// Конструктор кватерниона
        /// </summary>
        /// <param name="_v">The vector part</param>
        /// <param name="_w">Z компонент кватерниона</param>
        public UEIQuaternion(UEIVector3 _v, float _w)
        {
            this.X = _v.X;
            this.Y = _v.Y;
            this.Z = _v.Z;
            this.W = _w;
        }
        /// <summary>
        /// Конструктор кватерниона
        /// </summary>
        /// <param name="_x">X компонент кватерниона</param>
        /// <param name="_y">Y компонент кватерниона</param>
        /// <param name="_z">Z компонент кватерниона</param>
        /// <param name="_w">W компонент кватерниона</param>
        public UEIQuaternion(float _x, float _y, float _z, float _w)
        {
            this.X = _x;
            this.Y = _y;
            this.Z = _z;
            this.W = _w;
        }
        #region Funtion
        /// <summary>
        /// Установить новые значения для кватерниона
        /// </summary>
        /// <param name="new_x">Новый X компонент вектора</param>
        /// <param name="new_y">Новый Y компонент вектора</param>
        /// <param name="new_z">Новый Z компонент вектора</param>
        /// <param name="new_w">Новый W компонент вектора</param>
        public void Set(float new_x, float new_y, float new_z, float new_w)
        {
            this.X = new_x;
            this.Y = new_y;
            this.Z = new_z;
            this.W = new_w;
        }
        //
        // Summary:
        //     ///
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //     ///
        //
        // Parameters:
        //   fromDirection:
        //
        //   toDirection:
        public void SetFromToRotation(UEIVector3 fromDirection, UEIVector3 toDirection)
        {
            this = UEIQuaternion.FromToRotation(fromDirection, toDirection);
        }
        //
        // Summary:
        //     ///
        //     Creates a rotation with the specified forward and upwards directions.
        //     ///
        //
        // Parameters:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(UEIVector3 view)
        {
            UEIVector3 up = UEIVector3.up;
            this.SetLookRotation(view, up);
        }
        //
        // Summary:
        //     ///
        //     Creates a rotation with the specified forward and upwards directions.
        //     ///
        //
        // Parameters:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(UEIVector3 view, UEIVector3 up)
        {
            this = LookRotation(view, up);
        }
        public void ToAngleAxis(out float angle, out UEIVector3 axis)
        {
            Internal_ToAxisAngleRad(this, out axis, out angle);
            angle *= radToDeg;
        }
        /// <summary>
        /// Возвращает текстовое представление кватериниона
        /// </summary>
        /// <returns>результат</returns>
        public override string ToString()
        {
            object[] args = new object[] { this.X, this.Y, this.Z, this.W };
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", args);

        }
        #endregion

        #region Static Function
        //
        // Summary:
        //     ///
        //     Returns the angle in degrees between two rotations a and b.
        //     ///
        //
        // Parameters:
        //   a:
        //
        //   b:
        public static float Angle(UEIQuaternion a, UEIQuaternion b)
        {
            return (float)((Math.Acos(Math.Min(Math.Abs(Dot(a, b)), 1f)) * 2f) * 57.29578f);
        }
        //
        // Summary:
        //     ///
        //     Creates a rotation which rotates angle degrees around axis.
        //     ///
        //
        // Parameters:
        //   angle:
        //
        //   axis:
        public static UEIQuaternion AngleAxis(float angle, UEIVector3 axis)
        {
            return UEIQuaternion.INTERNAL_CALL_AngleAxis(angle, ref axis);
        }
        /// Скалярное произведение между двумя вращениями.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>результат</returns>
        public static float Dot(UEIQuaternion a, UEIQuaternion b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }
        //
        // Summary:
        //     ///
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis (in that order).
        //     ///
        //
        // Parameters:
        //   euler:
        public static UEIQuaternion Euler(UEIVector3 euler)
        {
            return Internal_FromEulerRad((UEIVector3)(euler * 0.01745329f));
        }
        //
        // Summary:
        //     ///
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis (in that order).
        //     ///
        //
        // Parameters:
        //   x:
        //
        //   y:
        //
        //   z:
        public static UEIQuaternion Euler(float x, float y, float z)
        {
            return Internal_FromEulerRad((UEIVector3)(new UEIVector3(x, y, z) * 0.01745329f));

        }
        // from http://stackoverflow.com/questions/12088610/conversion-between-euler-UEIQuaternionernion-like-in-unity3d-engine
        private static UEIVector3 Internal_ToEulerRad(UEIQuaternion rotation)
        {
            float sqw = rotation.W * rotation.W;
            float sqx = rotation.X * rotation.X;
            float sqy = rotation.Y * rotation.Y;
            float sqz = rotation.Z * rotation.Z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = rotation.X * rotation.W - rotation.Y * rotation.Z;
            UEIVector3 v;

            if (test > 0.4995f * unit)
            { // singularity at north pole
                v = new UEIVector3(
                    2.0f * (float)Math.Atan2(rotation.Y, rotation.X),
                    (float)Math.PI / 2,
                    0.0f);
                return NormalizeAngles(v * Rad2Deg);
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v = new UEIVector3(
                    -2f * (float)Math.Atan2(rotation.Y, rotation.X),
                    (float)-Math.PI / 2,
                    0);
                return NormalizeAngles(v * Rad2Deg);
            }
            UEIQuaternion q = new UEIQuaternion(rotation.W, rotation.Z, rotation.X, rotation.Y);
            v = new UEIVector3(
            (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W)),     // Yaw
            (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y)),                                           // Pitch
            (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z))      // Roll
            );
            return NormalizeAngles(v * Rad2Deg);
        }
        private static UEIVector3 NormalizeAngles(UEIVector3 angles)
        {
            angles = new UEIVector3(
            NormalizeAngle(angles.X),
            NormalizeAngle(angles.Y),
            NormalizeAngle(angles.Z));
            return angles;
        }

        private static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }

        // from http://stackoverflow.com/questions/11492299/UEIQuaternionernion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha
        private static UEIQuaternion Internal_FromEulerRad(UEIVector3 euler)
        {
            float yaw = euler.X;
            float pitch = euler.Y;
            float roll = euler.Z;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin((double)rollOver2);
            float cosRollOver2 = (float)Math.Cos((double)rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
            float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin((double)yawOver2);
            float cosYawOver2 = (float)Math.Cos((double)yawOver2);
            UEIQuaternion result = new UEIQuaternion(
                cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2,
                cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2,
                cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2,
                sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2);
            return result;

        }
        private static void Internal_ToAxisAngleRad(UEIQuaternion q, out UEIVector3 axis, out float angle)
        {
            if (Math.Abs(q.W) > 1.0f)
                q.Normalize();


            angle = 2.0f * (float)Math.Acos(q.W); // angle
            float den = (float)Math.Sqrt(1.0 - q.W * q.W);
            if (den > 0.0001f)
            {
                axis = q.XYZ / den;
            }
            else
            {
                // This occurs when the angle is zero. 
                // Not a problem: just set an arbitrary normalized axis.
                axis = new UEIVector3(1, 0, 0);
            }
        }
        private static UEIQuaternion INTERNAL_CALL_AngleAxis(float degress, ref UEIVector3 axis)
        {
            //if (axis.sqrMagnitude == 0.0f)
            if (Math.Abs(axis.sqrMagnitude) < float.Epsilon)
                return identity;

            UEIQuaternion result = identity;
            float radians = degress * degToRad;
            radians *= 0.5f;
            axis.Normalize();
            axis = axis * (float)Math.Sin(radians);
            result.X = axis.X;
            result.Y = axis.Y;
            result.Z = axis.Z;
            result.W = (float)Math.Cos(radians);

            return Normalize(result);
        }
        // from http://answers.unity3d.com/questions/467614/what-is-the-source-code-of-UEIQuaternionernionlookrotation.html
        private static UEIQuaternion INTERNAL_CALL_LookRotation(ref UEIVector3 forward, ref UEIVector3 up)
        {

            forward = UEIVector3.Normalize(forward);
            UEIVector3 right = UEIVector3.Normalize(UEIVector3.Cross(up, forward));
            up = UEIVector3.Cross(forward, right);
            float m00 = right.X;
            float m01 = right.Y;
            float m02 = right.Z;
            float m10 = up.X;
            float m11 = up.Y;
            float m12 = up.Z;
            float m20 = forward.X;
            float m21 = forward.Y;
            float m22 = forward.Z;


            float num8 = (m00 + m11) + m22;
            UEIQuaternion UEIQuaternionernion = new UEIQuaternion();
            if (num8 > 0f)
            {
                float num = (float)Math.Sqrt(num8 + 1f);
                UEIQuaternionernion.W = num * 0.5f;
                num = 0.5f / num;
                UEIQuaternionernion.X = (m12 - m21) * num;
                UEIQuaternionernion.Y = (m20 - m02) * num;
                UEIQuaternionernion.Z = (m01 - m10) * num;
                return UEIQuaternionernion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                float num7 = (float)Math.Sqrt(((1f + m00) - m11) - m22);
                float num4 = 0.5f / num7;
                UEIQuaternionernion.X = 0.5f * num7;
                UEIQuaternionernion.Y = (m01 + m10) * num4;
                UEIQuaternionernion.Z = (m02 + m20) * num4;
                UEIQuaternionernion.W = (m12 - m21) * num4;
                return UEIQuaternionernion;
            }
            if (m11 > m22)
            {
                float num6 = (float)Math.Sqrt(((1f + m11) - m00) - m22);
                float num3 = 0.5f / num6;
                UEIQuaternionernion.X = (m10 + m01) * num3;
                UEIQuaternionernion.Y = 0.5f * num6;
                UEIQuaternionernion.Z = (m21 + m12) * num3;
                UEIQuaternionernion.W = (m20 - m02) * num3;
                return UEIQuaternionernion;
            }
            float num5 = (float)Math.Sqrt(((1f + m22) - m00) - m11);
            float num2 = 0.5f / num5;
            UEIQuaternionernion.X = (m20 + m02) * num2;
            UEIQuaternionernion.Y = (m21 + m12) * num2;
            UEIQuaternionernion.Z = 0.5f * num5;
            UEIQuaternionernion.W = (m01 - m10) * num2;
            return UEIQuaternionernion;
        }
        private static UEIQuaternion INTERNAL_CALL_Slerp(ref UEIQuaternion a, ref UEIQuaternion b, float t)
        {
            if (t > 1) t = 1;
            if (t < 0) t = 0;
            return INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t);
        }
        private static UEIQuaternion INTERNAL_CALL_SlerpUnclamped(ref UEIQuaternion a, ref UEIQuaternion b, float t)
        {
            // if either input is zero, return the other.
            //if (a.LengthSquared == 0.0f)
            if (Math.Abs(a.LengthSquared) < float.Epsilon)
            {
                //if (b.LengthSquared == 0.0f)
                if (Math.Abs(b.LengthSquared) < float.Epsilon)
                {
                    return identity;
                }
                return b;
            }
            //else if (b.LengthSquared == 0.0f)
            else if (Math.Abs(b.LengthSquared) < float.Epsilon)
            {
                return a;
            }


            float cosHalfAngle = a.W * b.W + UEIVector3.Dot(a.XYZ, b.XYZ);

            if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
            {
                // angle = 0.0f, so just return one input.
                return a;
            }
            else if (cosHalfAngle < 0.0f)
            {
                b.XYZ = -1 * b.XYZ;
                b.W = -b.W;
                cosHalfAngle = -cosHalfAngle;
            }

            float blendA;
            float blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                float halfAngle = (float)Math.Acos(cosHalfAngle);
                float sinHalfAngle = (float)Math.Sin(halfAngle);
                float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = (float)Math.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
                blendB = (float)Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0f - t;
                blendB = t;
            }

            UEIQuaternion result = new UEIQuaternion(blendA * a.XYZ + blendB * b.XYZ, blendA * a.W + blendB * b.W);
            if (result.LengthSquared > 0.0f)
                return Normalize(result);
            else
                return identity;
        }
        #region public float Length

        /// <summary>
        /// Gets the length (magnitude) of the UEIQuaternionernion.
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
            }
        }

        #endregion
        #region public float LengthSquared

        /// <summary>
        /// Gets the square of the UEIQuaternionernion length (magnitude).
        /// </summary>
        public float LengthSquared
        {
            get
            {
                return X * X + Y * Y + Z * Z + W * W;
            }
        }

        #endregion

        #region public void Normalize()

        /// <summary>
        /// Scales the UEIQuaternionernion to unit length.
        /// </summary>
        public void Normalize()
        {
            float scale = 1.0f / this.Length;
            XYZ *= scale;
            W *= scale;
        }

        #endregion


        #region Normalize

        /// <summary>
        /// Scale the given UEIQuaternionernion to unit length
        /// </summary>
        /// <param name="q">The UEIQuaternionernion to normalize</param>
        /// <returns>The normalized UEIQuaternionernion</returns>
        public static UEIQuaternion Normalize(UEIQuaternion q)
        {
            UEIQuaternion result;
            Normalize(ref q, out result);
            return result;
        }

        /// <summary>
        /// Scale the given UEIQuaternionernion to unit length
        /// </summary>
        /// <param name="q">The UEIQuaternionernion to normalize</param>
        /// <param name="result">The normalized UEIQuaternionernion</param>
        public static void Normalize(ref UEIQuaternion q, out UEIQuaternion result)
        {
            float scale = 1.0f / q.Length;
            result = new UEIQuaternion(q.XYZ * scale, q.W * scale);
        }

        #endregion
        //
        // Summary:
        //     ///
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //     ///
        //
        // Parameters:
        //   fromDirection:
        //
        //   toDirection:
        public static UEIQuaternion FromToRotation(UEIVector3 fromDirection, UEIVector3 toDirection)
        {
            return RotateTowards(LookRotation(fromDirection), LookRotation(toDirection), float.MaxValue);
        }
        //
        // Summary:
        //     ///
        //     Returns the Inverse of rotation.
        //     ///
        //
        // Parameters:
        //   rotation:
        public static UEIQuaternion Inverse(UEIQuaternion rotation)
        {
            /*UEIQuaternion UEIQuaternionernion;
            INTERNAL_CALL_Inverse(ref rotation, out UEIQuaternion);
            return UEIQuaternion;*/
            float lengthSq = rotation.LengthSquared;

            //if (lengthSq != 0.0)
            if (Math.Abs(lengthSq) > float.Epsilon)
            {
                float i = 1.0f / lengthSq;
                return new UEIQuaternion(rotation.XYZ * -i, rotation.W * i);
            }
            return rotation;
        }
        //
        // Summary:
        //     ///
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is clamped to the range [0, 1].
        //     ///
        //
        // Parameters:
        //   a:
        //
        //   b:
        //
        //   t:
        public static UEIQuaternion Lerp(UEIQuaternion a, UEIQuaternion b, float t)
        {
            t = EMath.Clamp01(t);
            return new UEIQuaternion(a.X + ((b.X - a.X) * t), a.Y + ((b.Y - a.Y) * t), a.Z + ((b.Z - a.Z) * t), a.W + ((b.W - a.W) * t));
        }
        //
        // Summary:
        //     ///
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is not clamped.
        //     ///
        //
        // Parameters:
        //   a:
        //
        //   b:
        //
        //   t:
        public static UEIQuaternion LerpUnclamped(UEIQuaternion a, UEIQuaternion b, float t)
        {
            return new UEIQuaternion(a.X + ((b.X - a.X) * t), a.Y + ((b.Y - a.Y) * t), a.Z + ((b.Z - a.Z) * t), a.W + ((b.W - a.W) * t));

        }
        //
        // Summary:
        //     ///
        //     Creates a rotation with the specified forward and upwards directions.
        //     ///
        //
        // Parameters:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.
        public static UEIQuaternion LookRotation(UEIVector3 forward)
        {
            UEIQuaternion UEIQuaternion;
            UEIVector3 up = UEIVector3.up;
            UEIQuaternion = INTERNAL_CALL_LookRotation(ref forward, ref up);
            return UEIQuaternion;
        }
        //
        // Summary:
        //     ///
        //     Creates a rotation with the specified forward and upwards directions.
        //     ///
        //
        // Parameters:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.
        public static UEIQuaternion LookRotation(UEIVector3 forward, UEIVector3 upwards)
        {
            UEIQuaternion UEIQuaternion;
            UEIQuaternion = INTERNAL_CALL_LookRotation(ref forward, ref upwards);
            return UEIQuaternion;
        }
        //
        // Summary:
        //     ///
        //     Rotates a rotation from towards to.
        //     ///
        //
        // Parameters:
        //   from:
        //
        //   to:
        //
        //   maxDegreesDelta:
        public static UEIQuaternion RotateTowards(UEIQuaternion from, UEIQuaternion to, float maxDegreesDelta)
        {
            float num = Angle(from, to);


            //if (num == 0f)
            if (Math.Abs(num) < float.Epsilon)
            {
                return to;
            }
            float t = Math.Min((float)1f, (float)(maxDegreesDelta / num));
            return SlerpUnclamped(from, to, t);
        }
        //
        // Summary:
        //     ///
        //     Spherically interpolates between a and b by t. The parameter t is clamped to
        //     the range [0, 1].
        //     ///
        //
        // Parameters:
        //   a:
        //
        //   b:
        //
        //   t:
        public static UEIQuaternion Slerp(UEIQuaternion a, UEIQuaternion b, float t)
        {
            UEIQuaternion UEIQuaternion;
            UEIQuaternion = INTERNAL_CALL_Slerp(ref a, ref b, t);
            return UEIQuaternion;
        }
        //
        // Summary:
        //     ///
        //     Spherically interpolates between a and b by t. The parameter t is not clamped.
        //     ///
        //
        // Parameters:
        //   a:
        //
        //   b:
        //
        //   t:
        public static UEIQuaternion SlerpUnclamped(UEIQuaternion a, UEIQuaternion b, float t)
        {
            UEIQuaternion UEIQuaternion;
            UEIQuaternion = INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t);
            return UEIQuaternion;
        }
        #region Operators
        public static UEIQuaternion operator *(UEIQuaternion lhs, UEIQuaternion rhs)
        {
            return new UEIQuaternion((((lhs.W * rhs.X) + (lhs.X * rhs.W)) + (lhs.Y * rhs.Z)) - (lhs.Z * rhs.Y), (((lhs.W * rhs.Y) + (lhs.Y * rhs.W)) + (lhs.Z * rhs.X)) - (lhs.X * rhs.Z), (((lhs.W * rhs.Z) + (lhs.Z * rhs.W)) + (lhs.X * rhs.Y)) - (lhs.Y * rhs.X), (((lhs.W * rhs.W) - (lhs.X * rhs.X)) - (lhs.Y * rhs.Y)) - (lhs.Z * rhs.Z));
        }
        public static UEIVector3 operator *(UEIQuaternion rotation, UEIVector3 point)
        {
            float num = rotation.X * 2f;
            float num2 = rotation.Y * 2f;
            float num3 = rotation.Z * 2f;
            float num4 = rotation.X * num;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.X * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            UEIVector3 vector = new UEIVector3(
            (((1f - (num5 + num6)) * point.X) + ((num7 - num12) * point.Y)) + ((num8 + num11) * point.Z),
            (((num7 + num12) * point.X) + ((1f - (num4 + num6)) * point.Y)) + ((num9 - num10) * point.Z),
            (((num8 - num11) * point.X) + ((num9 + num10) * point.Y)) + ((1f - (num4 + num5)) * point.Z));
            return vector;
        }
        public static bool operator ==(UEIQuaternion lhs, UEIQuaternion rhs)
        {
            //return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W;
            return Math.Abs(lhs.X - rhs.X) < float.Epsilon &&
                Math.Abs(lhs.Y - rhs.Y) < float.Epsilon &&
                Math.Abs(lhs.Z - rhs.Z) < float.Epsilon &&
                Math.Abs(lhs.W - rhs.W) < float.Epsilon;
        }
        public static bool operator !=(UEIQuaternion lhs, UEIQuaternion rhs)
        {
            //return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W;
            return Math.Abs(lhs.X - rhs.X) > float.Epsilon ||
                Math.Abs(lhs.Y - rhs.Y) > float.Epsilon ||
                Math.Abs(lhs.Z - rhs.Z) > float.Epsilon ||
                Math.Abs(lhs.W - rhs.W) > float.Epsilon;
        }
        #endregion
        #endregion
    }
}