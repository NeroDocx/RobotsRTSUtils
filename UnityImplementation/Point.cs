// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace Utils.UnityImplementation
{
    /// <summary>
    /// Структура содержащая координаты точки
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Координата X (она же q)
        /// </summary>
        public int X;
        /// <summary>
        /// Координата Y (она же r)
        /// </summary>
        public int Y;
        /// <summary>
        /// Координата Z (она же s)
        /// </summary>
        public int Z;
        /// <summary>
        /// Котнструктор
        /// </summary>
        /// <param name="x">Координата X (q)</param>
        /// <param name="y">Координата Y (r)</param>
        /// <param name="z">Координата Y (s)</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Z = -x - y;
        }
        public static bool operator ==(Point lhs, Point rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }
        public static bool operator !=(Point lhs, Point rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y;
        }
    }
}