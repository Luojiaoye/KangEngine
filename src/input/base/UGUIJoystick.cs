using UnityEngine;
using UnityEngine.UI;
using KangEngine.Input.Data;

namespace KangEngine.Input.Base
{
    public class UGUIJoystick : JoystickBase
    {
        public UGUIData data = new UGUIData();
        public Image joystickImage = null;
        public Image joystickBackgroundImage = null;
        public RectTransform joystickRT = null;
        public RectTransform joystickBackgroundRT = null;
        public GameObject cameraObj;


        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            joystickImage.color = joystickBackgroundImage.color = UGUIData.COLOR_ZERO_ALL;
        }

        internal override void ControlAwake()
        {
            base.ControlAwake();
            data.GetRectAndImage(gameObject);
            data.camera = cameraObj.GetComponent<UnityEngine.Camera>();
            SetTransparency();
        }

        protected override void ShowingTouchZone()
        {
            if (showTouchZone)
                data.touchzoneImage.color = UGUIData.COLOR_HALF_SPRITE;
            else
                data.touchzoneImage.color = UGUIData.COLOR_ZERO_ALL;
        }

        private void SetTransparency()
        {
            if (isStatic || showTouchZone)
                joystickImage.color = joystickBackgroundImage.color = UGUIData.COLOR_HALF_SPRITE;
            else
                joystickImage.color = joystickBackgroundImage.color = UGUIData.COLOR_ZERO_ALL;
        }

        internal override bool CheckTouchPosition(Vector2 touchPos)
        {
            return data.CheckTouchPosition(touchPos);
        }

        protected override void GetCurrentPosition(Vector2 touchPos)
        {
            defaultPosition = currentPosition = joystickBackgroundRT.position;
            if (enableAxisX) currentPosition.x = data.camera.ScreenToWorldPoint(touchPos).x;
            if (enableAxisY) currentPosition.y = data.camera.ScreenToWorldPoint(touchPos).y;
        }

        protected override float CalculateBorderSize()
        {
            return (joystickBackgroundRT.sizeDelta.magnitude / 2f) * borderSize / 8f;
        }

        protected override void UpdateJoystickPosition()
        {
            joystickRT.position = currentPosition;
        }

        protected override void UpdateTransparencyAndPosition(Vector2 touchPos)
        {
            joystickImage.color = joystickBackgroundImage.color = UGUIData.COLOR_HALF_SPRITE;
            joystickRT.position = joystickBackgroundRT.position = data.camera.ScreenToWorldPoint(touchPos);
        }

        internal override void ControlReset()
        {
            base.ControlReset();
            SetTransparency();
        }
    }
}
