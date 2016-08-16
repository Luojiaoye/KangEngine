using UnityEngine;

namespace KangEngine.Render.Base
{
    public class TransformObject
    {
        private float _curRotationTime;
        private float _rotationTime;
        private float _rotationAngle;
        private Vector3 _rotationAxis;
        private Transform _transform;

        public int id
        {
            get { return _transform.GetInstanceID(); }
        }
        public float ratationTime
        {
            set { _rotationTime = value; }
        }

        public float rotationAngle
        {
            set { _rotationAngle = value; }
        }

        public Vector3 roationAxis
        {
            set { _rotationAxis = value; }
        }

        public TransformObject(Transform trans)
        {
            this._transform = trans;
            this._curRotationTime = 0f;
            this._rotationAngle = 0f;
            this._rotationTime = 0f;
        }

        public void Clear()
        {
            this._curRotationTime = 0f;
            this._rotationAngle = 0f;
            this._rotationTime = 0f;
        }

        public bool Update()
        {
            if (this._rotationAngle == 0 || this._curRotationTime >= this._rotationTime)
                return true;

            float deltaTime = Time.deltaTime;
            this._curRotationTime += Time.deltaTime;
            if (this._curRotationTime > this._rotationTime)
                deltaTime = this._curRotationTime - this._rotationTime;

            this._transform.Rotate(this._rotationAxis, (deltaTime / this._rotationTime) * this._rotationAngle);
            return false;
        }

    }
}
