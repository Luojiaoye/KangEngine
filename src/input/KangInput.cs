using UnityEngine;
using KangEngine.Camera;
using KangEngine.Camera.Base;
using KangEngine.Util;

namespace KangEngine.Input
{
    public static class KangInput
    {
        public static int rayDistance = 1000;
        public static bool TryGetMouseWorldPostion(out Vector3 position, int layer = -1)
        {
            position = Vector3.zero;

            KangCamera camera = KangCameraManager.Inst().GetKangCamera();
            if (camera == null)
                return false;

            Ray ray = camera.camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (layer == -1)
            {
                return KangRay.RaycastPoint(ray, rayDistance, out position);
            }
            else
            {
                LayerMask layerMask = (LayerMask)(((int)1) << layer);
                return KangRay.RaycastPoint(ray, rayDistance, layerMask, out position);
            }
        }

        public static bool MouseButtonDown(MouseType type)
        {
            if (type >= MouseType.MT_Invalid)
                return false;
            return UnityEngine.Input.GetMouseButtonDown((int)type);
        }

        public static float GetMouseAxis(MouseAxisType axisType)
        {
            switch (axisType)
            {
                case MouseAxisType.MT_MouseX:
                    return UnityEngine.Input.GetAxis("Mouse Y");
                case MouseAxisType.MT_MouseY:
                    return UnityEngine.Input.GetAxis("Mouse X");
            }
            return 0.0f;
        }
    }

    public enum MouseType
    {
        MT_Left,
        MT_Right,
        MT_Middle,
        MT_Invalid,
    }

    public enum MouseAxisType
    {
        MT_MouseX,
        MT_MouseY,
    }

    public enum AxisType
    {
        AT_Unknow,
        AT_Horizontal,
        AT_Vertical,
        AT_Default = AT_Horizontal,
    }
}
