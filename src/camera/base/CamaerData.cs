using UnityEngine;

namespace KangEngine.Camera.Base
{
    public class CameraData
    {
    }

    public struct SmoothFollowData
    {
        //public float offsetDistance;
        //public float offsetHeight;
        public float curAngle;
        public float angleSpeed;
        public Vector3 offsetPosition;
        public float heightDampValue;
        public float angleDampValue;
        public Vector3 offsetRotation;
    }

    public struct StaticFollowData
    {
        public Vector3 offsetPosition;
        public Vector3 offsetAngles;
        public float staticFollowLerpTime;
    }

    public struct KangFollowData
    {
        public float limitOffsetAngle;
        public float curOffsetAngle;
        public float rotateSpeed;
        public Vector3 offsetPosition;
        public float kangLerpTime;
        public float radius;
        public float distance;
        public Vector3 initRotation;
    }

    public struct GhostFollowData
    {
        public float axisXSensitivity;
        public float axisYSensitivity;
        public float initSpeed;
        public float increseSpeed;
    }
}
