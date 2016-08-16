using System;
using UnityEngine;
using KangEngine.Camera.Base;
using KangEngine.Input;

namespace KangEngine.Camera.Base
{
    public sealed class KangCamera
    {
        public static StaticFollowData DEFAULT_STATIC_FOLLOW_DATA = new StaticFollowData { offsetPosition = new Vector3(0.36f, 171.8f, -135.33f), staticFollowLerpTime = 0.1f, offsetAngles = new Vector3(32f, 0f, 0f) };
        public static SmoothFollowData DEFAULT_SMOOTH_FOLLOW_DATA = new SmoothFollowData { angleDampValue = 1.3f, heightDampValue = 0.1f, offsetPosition = Vector3.one, offsetRotation = Vector3.zero };
        public static KangFollowData DEFAULT_KANG_FOLLOW_DATA = new KangFollowData { limitOffsetAngle = 30f, curOffsetAngle = 0f, rotateSpeed = 20.0f, offsetPosition = new Vector3(0, 54.3f, -53.7f), kangLerpTime = 1f, radius = 2.0f, distance = 0f };
        public static GhostFollowData DEFAULT_GHOST_FOLLOW_DATA = new GhostFollowData { increseSpeed = 2.25f, initSpeed = 30.0f, axisXSensitivity = 0.025f, axisYSensitivity = 0.025f };

        private uint _id;
        private CameraFollowType _followType;
        private CameraType _camerType;
        private UnityEngine.Camera _coreCamera;
        private Transform _target;

        private StaticFollowData _staticFollowData = DEFAULT_STATIC_FOLLOW_DATA;
        private SmoothFollowData _smoothFollowData = DEFAULT_SMOOTH_FOLLOW_DATA;
        private KangFollowData _kangFollowData = DEFAULT_KANG_FOLLOW_DATA;
        private GhostFollowData _ghostFollowData = DEFAULT_GHOST_FOLLOW_DATA;

        private Delegate _customUpdate;
        private object _customUpdateObj;
        private object[] _customUpdateArgs;

        private bool _lastMoving = false;
        private bool _moving = false;
        private float _curSpeed;

        public KangCamera(uint id, CameraType type, UnityEngine.Camera camera)
        {
            _id = id;
            _camerType = type;
            _coreCamera = camera;
        }

        public void SetCustomUpdate(object thisObj, Delegate callback, object[] args)
        {
            this._customUpdateObj = thisObj;
            this._customUpdate = callback;
            this._customUpdateArgs = args;
        }

        public bool Update()
        {
            if (this._customUpdateObj == null)
                return false;

            this._customUpdate.Method.Invoke(this._customUpdateObj, this._customUpdateArgs);
            return true;
        }

        public void Follow()
        {
            switch (this._followType)
            {
                case CameraFollowType.CFT_Static:
                    StaticFollow();
                    break;
                case CameraFollowType.CFT_Tween:
                    TweenFollow();
                    break;
                case CameraFollowType.CTF_Kang:
                    KangFollow();
                    break;
                case CameraFollowType.CTF_Ghost:
                    GhostFollow();
                    break;
            }
        }

        private void StaticFollow()
        {
            if (this._target == null)
                return;

            position = Vector3.Lerp(this.position, this._target.position + this._staticFollowData.offsetPosition, this._staticFollowData.staticFollowLerpTime);
        }

        private void TweenFollow()
        {
            if (this._target == null)
                return;

            float curHeight = transform.position.y;
            float curAngleY = transform.eulerAngles.y;

            float destHeight = this._target.position.y + this._smoothFollowData.offsetPosition.y;
            float destAngleY = this._target.eulerAngles.y;

            float curAngleVel = 0.0f;
            curAngleY = Mathf.SmoothDampAngle(curAngleY, destAngleY, ref curAngleVel, this._smoothFollowData.angleDampValue);
            float curHeightVel = 0.0f;
            curHeight = Mathf.SmoothDamp(curHeight, destHeight, ref curHeightVel, this._smoothFollowData.heightDampValue);

            //Quaternion quaternion = Quaternion.Euler(this._smoothFollowData.offsetRotation.x, curAngleY + this._smoothFollowData.offsetRotation.y, this._smoothFollowData.offsetRotation.z);
            //transform.position = this._target.position;
            //transform.position -= quaternion * this._smoothFollowData.offsetPosition;
            //transform.position = new Vector3(transform.position.x, curHeight, transform.position.z);

            transform.position = Vector3.Lerp(this.position, this._target.position + this._smoothFollowData.offsetPosition, this._smoothFollowData.heightDampValue);

            //float destX = this._target.eulerAngles.x + this._smoothFollowData.offsetRotation.x;
            //float destY = this._target.eulerAngles.y + this._smoothFollowData.offsetRotation.y;
            //float destZ = this._target.eulerAngles.z + this._smoothFollowData.offsetRotation.z;
            //float tmpX = 0.0f;
            //float curX = this.transform.eulerAngles.x;
            //curX = Mathf.SmoothDampAngle(curX, destX, ref tmpX, this._smoothFollowData.angleDampValue);
            //float tmpY = 0f;
            //float curY = this.transform.eulerAngles.y;
            //curY = Mathf.SmoothDampAngle(curY, destY, ref tmpY, this._smoothFollowData.angleDampValue);
            //float tmpZ = 0f;
            //float curZ = this.transform.eulerAngles.z;
            //curZ = Mathf.SmoothDampAngle(curZ, destZ, ref tmpZ, this._smoothFollowData.angleDampValue);
            //Vector3 destRotation = this._target.rotation.eulerAngles + this._smoothFollowData.offsetRotation;
            //Vector3 rotationAngles = Vector3.Lerp(this.transform.rotation.eulerAngles, destRotation, this._smoothFollowData.angleDampValue);
            //transform.rotation = Quaternion.Euler(rotationAngles);
            if (this._smoothFollowData.curAngle > 0)
            {
                //transform.RotateAround(this._target.transform.position, Vector3.up, this._smoothFollowData.angleSpeed * Time.deltaTime);
                //Quaternion qua = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + this._smoothFollowData.curAngle, this.transform.rotation.eulerAngles.z);
                //transform.rotation = Quaternion.Lerp(transform.rotation, qua, this._smoothFollowData.angleDampValue);

                transform.Rotate(Vector3.up, this._smoothFollowData.curAngle, Space.World);
            }
                
            //transform.LookAt(this._target);
        }

        private void KangFollow()
        {
            if (this._target == null || this._kangFollowData.distance < this._kangFollowData.radius)
                return;

            position = Vector3.Lerp(this.position, this._target.position + this._kangFollowData.offsetPosition, this._kangFollowData.kangLerpTime);

            if (this._kangFollowData.curOffsetAngle <= this._kangFollowData.limitOffsetAngle)
            {
                this.transform.LookAt(this._target.transform);
                return;
            }

            float angle = this._kangFollowData.rotateSpeed * Time.deltaTime;
            this._kangFollowData.curOffsetAngle -= angle;
            this.transform.RotateAround(this.position, this._target.up, angle);
        }

        private void GhostFollow()
        {
            if (this._target == null)
                return;

            Vector3 angles = this.transform.eulerAngles;
            angles.x += KangInput.GetMouseAxis(MouseAxisType.MT_MouseX) * 359f * this._ghostFollowData.axisXSensitivity;
            angles.x += KangInput.GetMouseAxis(MouseAxisType.MT_MouseY) * 359f * this._ghostFollowData.axisYSensitivity;
            this.transform.rotation = Quaternion.Euler(angles);

            this._lastMoving = this._moving;
            if (this._moving)
            {
                _curSpeed += this._ghostFollowData.increseSpeed * Time.deltaTime;
                this._moving = false;
            }

            Vector3 moveVec = Vector3.zero;
            CheckMove(KeyCode.W, ref moveVec, Vector3.forward);
            CheckMove(KeyCode.S, ref moveVec, -Vector3.forward);
            CheckMove(KeyCode.A, ref moveVec, -Vector3.right);
            CheckMove(KeyCode.D, ref moveVec, Vector3.right);
            if (this._moving)
            {
                if (!this._lastMoving)
                    this._curSpeed = this._ghostFollowData.initSpeed;

                this.position += moveVec * this._curSpeed * Time.deltaTime;
            }
            else
                this._curSpeed = 0f;
        }

        private void CheckMove(KeyCode key, ref Vector3 deltaMove, Vector3 dir)
        {
            if (UnityEngine.Input.GetKey(key))
            {
                this._moving = true;
                deltaMove += dir;
            }
        }

        public uint id
        {
            get { return _id; }
        }

        public UnityEngine.Camera camera
        {
            get { return _coreCamera; }
        }

        public CameraType type
        {
            get { return _camerType; }
        }

        public CameraFollowType followType
        {
            get { return _followType; }
            set { _followType = value; }
        }

        public float fieldOfView
        {
            get { return _coreCamera.fieldOfView; }
            set { _coreCamera.fieldOfView = value; }
        }

        public Transform transform
        {
            get { return _coreCamera.transform; }
        }

        public Vector3 position
        {
            get { return _coreCamera.transform.position; }
            set { _coreCamera.transform.position = value; }
        }

        public StaticFollowData staticFollowData
        {
            get { return this._staticFollowData; }
            set
            {
                this._staticFollowData = value;
                this.transform.Rotate(this._staticFollowData.offsetAngles);
            }
        }

        public SmoothFollowData smoothFollowData
        {
            get { return this._smoothFollowData; }
            set
            {
                this._smoothFollowData = value;
                //if (this._smoothFollowData.curAngle > 0)
                //{
                //    Quaternion qua = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + this._smoothFollowData.curAngle, this.transform.rotation.eulerAngles.z);
                //    transform.rotation = Quaternion.Lerp(transform.rotation, qua, this._smoothFollowData.angleDampValue);
                //}
            }
        }

        public KangFollowData kangFollowData
        {
            get { return this._kangFollowData; }
            set
            {
                this._kangFollowData = value;
                this._coreCamera.transform.rotation = Quaternion.Euler(this._kangFollowData.initRotation);
            }
        }

        public GhostFollowData ghostFollowData
        {
            get { return this._ghostFollowData; }
            set { this._ghostFollowData = value; }
        }

        public Transform target
        {
            get { return _target; }
            set { _target = value; }
        }
    }

    public enum CameraType
    {
        CT_FirstView,
        CT_ThirdView,
        CT_FreeView,
    }

    public enum CameraFollowType
    {
        CFT_Static,
        CFT_Tween,
        CTF_Kang,
        CTF_Ghost,
    }
}
