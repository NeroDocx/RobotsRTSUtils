// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;

namespace Utils.UnityImplementation
{
    /// <summary>
    /// Структура Вектор. Трехкомпонентный вектор XYZ
    /// </summary>
    public struct UEIVector3
    {
        #region Values
        public const float kEpsilon = 1E-05F;
        /// <summary>
        /// X компонент вектора
        /// </summary>
        public float X { get; private set; }
        /// <summary>
        /// Y компонент вектора
        /// </summary>
        public float Y { get; private set; }
        /// <summary>
        /// Z компонент вектора
        /// </summary>
        public float Z { get; private set; }
        /// <summary>
        /// Возвращает Модуль вектора (vector magnitude)
        /// </summary>
        public float magnitude
        {
            get
            {
                //return Magnitude(this);
                return EMath.Sqrt(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
            }
        }
        /// <summary>
        /// возвращает вектор с величиной (magnitude) = 1
        /// </summary>
        public UEIVector3 normalized
        {
            get
            {
                return Normalize(this);
            }
        }
        /// <summary>
        /// Возвращает Модуль вектора (vector magnitude) без квадратного корня
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                //return SqrMagnitude(this);
                return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));

            }
        }
        #endregion 
        #region Static Values
        /// <summary>
        /// Вернет UEIVector3(0, 0, -1).
        /// </summary>
        public static UEIVector3 back
        {
            get
            {
                return new UEIVector3(0f, 0f, -1f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(0, -1, 0).
        /// </summary>
        public static UEIVector3 down
        {
            get
            {
                return new UEIVector3(0f, -1f, 0f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(0, 0, 1).
        /// </summary>
        public static UEIVector3 forward
        {
            get
            {
                return new UEIVector3(0f, 0f, 1f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(-1, 0, 0).
        /// </summary>
        public static UEIVector3 left
        {
            get
            {
                return new UEIVector3(-1f, 0f, 0f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(1, 1, 1).
        /// </summary>
        public static UEIVector3 one
        {
            get
            {
                return new UEIVector3(1f, 1f, 1f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(1, 0, 0).
        /// </summary>
        public static UEIVector3 right
        {
            get
            {
                return new UEIVector3(1f, 0f, 0f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(0, 1, 0).
        /// </summary>
        public static UEIVector3 up
        {
            get
            {
                return new UEIVector3(0f, 1f, 0f);
            }
        }
        /// <summary>
        /// Вернет UEIVector3(0, 0, 0).
        /// </summary>
        public static UEIVector3 zero
        {
            get
            {
                return new UEIVector3(0f, 0f, 0f);
            }
        }
        #endregion

        /// <summary>
        /// Конструктор Вектора
        /// </summary>
        /// <param name="_x">X компонент вектора</param>
        /// <param name="_y">Y компонент вектора</param>
        /// <param name="_z">Z компонент вектора</param>
        public UEIVector3(float _x = 0.0f, float _y = 0.0f, float _z = 0.0f)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
        /// <summary>
        /// Конструктор Вектора 
        /// </summary>
        /// <param name="_component">XYZ компонент вектора</param>
        public UEIVector3(float _component)
        {
            X = _component;
            Y = _component;
            Z = _component;
        }

        #region Funtion
        public override bool Equals(object other)
        {
            if (!(other is UEIVector3))
            {
                return false;
            }
            UEIVector3 vector = (UEIVector3)other;
            return ((this.X.Equals(vector.X) && this.Y.Equals(vector.Y)) && this.Z.Equals(vector.Z));

        }
        public override int GetHashCode()
        {
            return ((this.X.GetHashCode() ^ (this.Y.GetHashCode() << 2)) ^ (this.Z.GetHashCode() >> 2));

        }
        /// <summary>
        /// Нормализует вектор приводя его величину (magnitude) к 1
        /// </summary>
        public void Normalize()
        {
            //float SquareSum = X * X + Y * Y + Z * Z;
            //if (SquareSum > kEpsilon)
            //{
            //    float Scale = (float)(1.0d / Math.Sqrt(SquareSum));
            //    X *= Scale;
            //    Y *= Scale;
            //    Z *= Scale;
            //}
            float num = Magnitude(this);
            if (num > 1E-05f)
            {
                this = (UEIVector3)(this / num);
            }
            else
            {
                this = zero;
            }

        }
        /// <summary>
        /// Умножает каждый компонент этого вектора на такой же компоненте вектора масштаба.
        /// </summary>
        /// <param name="scale">Вектор масшатаба</param>
        public void Scale(UEIVector3 scale)
        {
            this.X *= scale.X;
            this.Y *= scale.Y;
            this.Z *= scale.Z;
        }
        /// <summary>
        /// Установить новые значения для вектора
        /// </summary>
        /// <param name="new_x">Новый X компонент вектора</param>
        /// <param name="new_y">Новый Y компонент вектора</param>
        /// <param name="new_z">Новый Z компонент вектора</param>
        public void Set(float new_x, float new_y, float new_z)
        {
            this.X = new_x;
            this.Y = new_y;
            this.Z = new_z;
        }
        /// <summary>
        /// Возвращает текстовое представление вектора
        /// </summary>
        /// <returns>результат</returns>
        public override string ToString()
        {
            object[] args = new object[] { this.X, this.Y, this.Z };
            return string.Format("({0:F1}, {1:F1}, {2:F1})", args);
        }
        #endregion

        #region Static Function
        /// <summary>
        /// Возвращает угол в между двумя векторами в градусах
        /// </summary>
        /// <param name="from">Первый вектор</param>
        /// <param name="to">Второй вектор</param>
        /// <returns>Угол в градусах</returns>
        public static float Angle(UEIVector3 from, UEIVector3 to)
        {
            //return (float)(180.0f / Math.PI * Math.Acos((from.X * to.X + from.Y * to.Y + from.Z * to.Z) /
            //    (Magnitude(from) * Magnitude(to))));
            return (float)(Math.Acos(EMath.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);

        }
        /// <summary>
        /// Возвращает копию вектора с его модулем, приведыннм к maxLength 
        /// </summary>
        /// <param name="vector">Вектор</param>
        /// <param name="maxLength">maxLength</param>
        /// <returns></returns>
        public static UEIVector3 ClampMagnitude(UEIVector3 vector, float maxLength)
        {

            //if (maxLength < kEpsilon)
            //{
            //    return UEIVector3.zero;
            //}

            //float VSq = vector.sqrMagnitude;
            //if (VSq > (maxLength * maxLength))
            //{
            //    float Scale = maxLength * 1 / (float)Math.Sqrt(VSq);
            //    return new UEIVector3(vector.X * Scale, vector.Y * Scale, vector.Z * Scale);
            //}
            //else
            //{
            //    return vector;
            //}
            if (vector.sqrMagnitude > (maxLength * maxLength))
            {
                return (UEIVector3)(vector.normalized * maxLength);
            }
            return vector;

        }
        /// <summary>
        /// Cross Product для двух вектров
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static UEIVector3 Cross(UEIVector3 lhs, UEIVector3 rhs)
        {
            //return lhs ^ rhs;
            return new UEIVector3((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y), (lhs.Z * rhs.X) - (lhs.X * rhs.Z), (lhs.X * rhs.Y) - (lhs.Y * rhs.X));

        }
        /// <summary>
        /// Возвращает вектор расстояния между a и b
        /// </summary>
        /// <param name="a">Первый вектор</param>
        /// <param name="b">Второй вектор</param>
        /// <returns></returns>
        public static float Distance(UEIVector3 a, UEIVector3 b)
        {
            //return (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y) + (b.Z - a.Z) * (b.Z - a.Z);
            UEIVector3 vector = new UEIVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            return EMath.Sqrt(((vector.X * vector.X) + (vector.Y * vector.Y)) + (vector.Z * vector.Z));

        }
        /// <summary>
        /// Возвращает Dot Product для двух векторов
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float Dot(UEIVector3 lhs, UEIVector3 rhs)
        {
            return (((lhs.X * rhs.X) + (lhs.Y * rhs.Y)) + (lhs.Z * rhs.Z));
        }
        /// <summary>
        /// Возвращает линейную интерполяцию между двумя векторами
        /// </summary>
        /// <param name="a">первый вектор</param>
        /// <param name="b">Второй вектор</param>
        /// <param name="t"></param>
        /// <returns>Реузльтат</returns>
        public static UEIVector3 Lerp(UEIVector3 a, UEIVector3 b, float t)
        {
            //return (a * (1.0f - t) + b * t).normalized;
            t = EMath.Clamp01(t);
            return new UEIVector3(a.X + ((b.X - a.X) * t), a.Y + ((b.Y - a.Y) * t), a.Z + ((b.Z - a.Z) * t));

        }
        /// <summary>
        /// Возвращает Модуль вектора (vector magnitude)
        /// </summary>
        /// <param name="a">Vector</param>
        /// <returns>Модуль вектора (vector magnitude)</returns>
        public static float Magnitude(UEIVector3 a)
        {
            //return (float)Math.Sqrt(SqrMagnitude(a));
            return (float)Math.Sqrt(((a.X * a.X) + (a.Y * a.Y)) + (a.Z * a.Z));

        }
        /// <summary>
        /// Возвращает вектор который длинне.
        /// </summary>
        /// <param name="lhs">первый вектор</param>
        /// <param name="rhs">второй вектор</param>
        /// <returns>самый длинный вектор</returns>
        public static UEIVector3 Max(UEIVector3 lhs, UEIVector3 rhs)
        {
            //return lhs.sqrMagnitude <= rhs.sqrMagnitude ? rhs : lhs;
            return new UEIVector3(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y), Math.Max(lhs.Z, rhs.Z));

        }
        /// <summary>
        /// Возвращает вектор который меньше.
        /// </summary>
        /// <param name="lhs">первый вектор</param>
        /// <param name="rhs">второй вектор</param>
        /// <returns>самый короткий вектор</returns>
        public static UEIVector3 Min(UEIVector3 lhs, UEIVector3 rhs)
        {
            //return lhs.sqrMagnitude >= rhs.sqrMagnitude ? rhs : lhs;
            return new UEIVector3(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y), Math.Min(lhs.Z, rhs.Z));

        }
        /// <summary>
        /// Перемещение текущей точки по прямой линии к целевой точке.
        /// </summary>
        /// <param name="current">Текущая точка</param>
        /// <param name="target">Целевая точка</param>
        /// <param name="maxDistanceDelta">Delta</param>
        /// <returns>результат</returns>
        public static UEIVector3 MoveTowards(UEIVector3 current, UEIVector3 target, float maxDistanceDelta)
        {
            //return new UEIVector3(
            //    current.X + (target.X - current.X) * maxDistanceDelta,
            //    current.Y + (target.Y - current.Y) * maxDistanceDelta,
            //    current.Z + (target.Z - current.Z) * maxDistanceDelta);

            UEIVector3 vector = target - current;
            float magnitude = vector.magnitude;

            //if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
            if ((magnitude > maxDistanceDelta) && (Math.Abs(magnitude) > float.Epsilon))
            {
                return (current + ((UEIVector3)((vector / magnitude) * maxDistanceDelta)));
            }
            return target;

        }
        /// <summary>
        /// Нормализовать вектор
        /// </summary>
        /// <param name="value"></param>
        /// <returns>результат</returns>
        public static UEIVector3 Normalize(UEIVector3 value)
        {
            //float SquareSum = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
            //if (SquareSum > kEpsilon)
            //{
            //    float Scale = (float)(1.0d / Math.Sqrt(SquareSum));
            //    return new UEIVector3(value.X * Scale, value.Y * Scale, value.Z * Scale);
            //}
            //return value;
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return (UEIVector3)(value / num);
            }
            return zero;

        }
        /// <summary>
        /// Возвращает копию вектора vector проецируется на вектор onNormal.
        /// </summary>
        /// <param name="vector">Исходный вектор</param>
        /// <param name="onNormal">векотор на который производится проекция</param>
        /// <returns>Результирующий вектор</returns>
        public static UEIVector3 Project(UEIVector3 vector, UEIVector3 onNormal)
        {
            //return (onNormal * ((vector.X * onNormal.X + vector.Y * onNormal.Y + vector.Z * onNormal.Z) /
            //    (onNormal.X * onNormal.X + onNormal.Y * onNormal.Y + onNormal.Z * onNormal.Z)));

            float num = Dot(onNormal, onNormal);
            if (num < EMath.Epsilon)
            {
                return zero;
            }
            return (UEIVector3)((onNormal * Dot(vector, onNormal)) / num);

        }
        /// <summary>
        /// Возвращает копию этого вектора проецирующегося на входной вектор
        /// Проецирует вектор на плоскости, определяемой нормальным ортогональна плоскости.
        /// </summary>
        /// <param name="vector">Исходный вектор</param>
        /// <param name="planeNormal"></param>
        /// <returns>Результирующий вектор</returns>
        public static UEIVector3 ProjectOnPlane(UEIVector3 vector, UEIVector3 planeNormal)
        {
            //return (planeNormal * (vector.X * planeNormal.X + vector.Y * planeNormal.Y + vector.Z * planeNormal.Z));
            return (vector - Project(vector, planeNormal));

        }
        /// <summary>
        /// Умножение двух векторов покомпонентно.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Итоговый вектор</returns>
        public static UEIVector3 Scale(UEIVector3 a, UEIVector3 b)
        {
            return new UEIVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        /// <summary>
        /// Возвращает Модуль вектора (vector magnitude) без квадратного корня
        /// </summary>
        /// <param name="a">Vector</param>
        /// <returns>Модуль вектора (vector magnitude) без квадратного корня</returns>
        public static float SqrMagnitude(UEIVector3 a)
        {
            return (((a.X * a.X) + (a.Y * a.Y)) + (a.Z * a.Z));
        }
        /// <summary>
        /// Середина отрезка между двумя векторами
        /// </summary>
        /// <param name="a">Vector a </param>
        /// <param name="b">Vector b</param>
        /// <returns>результат</returns>
        public static UEIVector3 MidPoint(UEIVector3 a, UEIVector3 b)
        {
            return new UEIVector3(
                (a.X + b.X) / 2,
                (a.Y + b.Y) / 2,
                (a.Z + b.Z) / 2);
        }
        #region Operators
        public static UEIVector3 operator +(UEIVector3 a, UEIVector3 b)
        {
            return new UEIVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static UEIVector3 operator -(UEIVector3 a, UEIVector3 b)
        {
            return new UEIVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static UEIVector3 operator *(float d, UEIVector3 a)
        {
            return new UEIVector3(a.X * d, a.Y * d, a.Z * d);
        }
        public static UEIVector3 operator *(UEIVector3 a, float d)
        {
            return new UEIVector3(a.X * d, a.Y * d, a.Z * d);
        }
        public static UEIVector3 operator /(UEIVector3 a, float d)
        {
            return new UEIVector3(a.X / d, a.Y / d, a.Z / d);
        }
        public static bool operator ==(UEIVector3 lhs, UEIVector3 rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < kEpsilon && Math.Abs(lhs.Y - rhs.Y) < kEpsilon && Math.Abs(lhs.Z - rhs.Z) < kEpsilon;
        }
        public static bool operator !=(UEIVector3 lhs, UEIVector3 rhs)
        {
            return Math.Abs(lhs.X - rhs.X) >= kEpsilon || Math.Abs(lhs.Y - rhs.Y) >= kEpsilon || Math.Abs(lhs.Z - rhs.Z) >= kEpsilon;
        }
        public static UEIVector3 operator ^(UEIVector3 lhs, UEIVector3 rhs)
        {
            return new UEIVector3(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X
                );
        }
        #endregion
        #endregion   
    }
}