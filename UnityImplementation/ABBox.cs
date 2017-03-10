// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace Utils.UnityImplementation
{
    /// <summary>
    /// Box Collider
    /// </summary>
    public class ABBox
    {
        /// <summary>
        /// Исходная A компонента
        /// </summary>
        private UEIVector3 aPoint;
        /// <summary>
        /// Исходная B компонента
        /// </summary>
        private UEIVector3 bPoint;
        /// <summary>
        /// Текущая  A компонента
        /// </summary>
        public UEIVector3 APoint { get; private set; }
        /// <summary>
        /// Текущая B компонента
        /// </summary>
        public UEIVector3 BPoint { get; private set; }
        /// <summary>
        /// Кострутор
        /// </summary>
        /// <param name="_aPoint">A компонента</param>
        /// <param name="_bPoint">B компонента</param>
        public ABBox(UEIVector3 _aPoint, UEIVector3 _bPoint)
        {
            aPoint = _aPoint;
            bPoint = _bPoint;

            APoint = _aPoint;
            BPoint = _bPoint;
        }
        /// <summary>
        /// Преобразовать Box для нового UEITransform
        /// </summary>
        /// <param name="_transform">_transform обьекта к котрому привязан box</param>
        public void SetNewTransform(UEITransform _transform)
        {
            APoint = UEIUtils.QuatTransformVector(_transform.rotation, aPoint);
            BPoint = UEIUtils.QuatTransformVector(_transform.rotation, bPoint);

            APoint += _transform.position;
            BPoint += _transform.position;
        }
    }
}
