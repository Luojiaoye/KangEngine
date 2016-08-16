using KangEngine.Core;
using KangEngine.Camera.Base;
using System.Collections.Generic;
using UnityEngine;

namespace KangEngine.Camera
{
    public class KangCameraManager : KangSingleTon<KangCameraManager>
    {
        private Dictionary<uint, KangCamera> _cameraDic;
        private KangCamera _curKangCamera;

        public KangCameraManager()
        {
            _cameraDic = new Dictionary<uint, KangCamera>();
            _curKangCamera = null;
        }
        public KangCamera CreateKangCamera(uint id, CameraType type, UnityEngine.Camera camera)
        {
            if (_cameraDic.ContainsKey(id))
                return null;

            return new KangCamera(id, type, camera);
        }

        public bool AddKangCamera(KangCamera camera)
        {
            if (camera == null)
                return false;

            if (_cameraDic.ContainsKey(camera.id))
                return false;

            _cameraDic[camera.id] = camera;
            return true;
        }

        public void RemoveKangCamera(uint id)
        {
            KangCamera camera = null;
            _cameraDic.TryGetValue(id, out camera);
            if (camera != null)
                _cameraDic.Remove(id);
        }

        public void RemoveKangCamera(KangCamera camera)
        {
            if (camera == null)
                return;

            RemoveKangCamera(camera.id);
        }

        public bool SetKangCamera(uint id)
        {
            KangCamera camera = null;
            _cameraDic.TryGetValue(id, out camera);
            if (camera != null)
            {
                this._curKangCamera = camera;
                return true;
            }
            return false;
        }

        public bool SetKangCamera(KangCamera camera)
        {
            if (camera == null)
                return false;

            return SetKangCamera(camera.id);
        }

        public KangCamera GetKangCamera()
        {
            return _curKangCamera;
        }

        public KangCamera GetKangCamera(uint id)
        {
            KangCamera camera = null;
            _cameraDic.TryGetValue(id, out camera);
            return camera;
        }

        public bool SetCameraTarget(Transform trans)
        {
            if (this._curKangCamera == null)
                return false;

            _curKangCamera.target = trans;
            return true;
        }

        public bool SetCameraTarget(uint id, Transform trans)
        {
            KangCamera camera = null;
            _cameraDic.TryGetValue(id, out camera);
            if (camera == null)
                return false;

            camera.target = trans;
            return true;
        }

        public void Update()
        {
            if (this._curKangCamera == null)
                return;

            this._curKangCamera.Update();
        }

        public void FixedUpdate()
        {
            if (_curKangCamera != null)
                _curKangCamera.Follow();
        }
    }
}
