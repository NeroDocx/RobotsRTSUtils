// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace Utils.UnityImplementation
{
    public enum Space
    {
        World,
        Self
    }

    public class UEITransform
    {
        public Point CurrentNode;
        public UEIVector3 position { get; set; }
        public UEIQuaternion rotation { get; set; }
        public UEITransform(UEIVector3 _position)
        {
            position = _position;
            rotation = new UEIQuaternion();
        }
        public UEITransform(UEIVector3 _position, UEIQuaternion _UEIQuaternion)
        {
            position = _position;
            rotation = _UEIQuaternion;
        }

        public UEIVector3 Forward
        {
            get
            {
                return (UEIVector3)(this.rotation * UEIVector3.forward);
            }
            set
            {
                this.rotation = UEIQuaternion.LookRotation(value);
            }
        }
        /*private void LookAt(UEITransform target)
        {
            UEIVector3 up = UEIVector3.up;
            this.LookAt(target, up);
        }
        private void LookAt(UEITransform target, UEIVector3 worldUp)
        {
            if (target != null)
            {
                this.LookAt(target.position, worldUp);
            }
        }
        private void LookAt(UEIVector3 worldPosition, UEIVector3 worldUp)
        {

        }*/

        public void Rotate(UEIVector3 _eulerAngels)
        {
            Space self = Space.Self;
            this.Rotate(_eulerAngels, self);
        }
        public void Rotate(UEIVector3 _eulerAngels, Space _relativeTo)
        {
            UEIQuaternion UEIQuaternion = UEIQuaternion.Euler(_eulerAngels.X, _eulerAngels.Y, _eulerAngels.Z);
        }
    }
}