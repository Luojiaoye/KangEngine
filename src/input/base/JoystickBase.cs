using UnityEngine;
using KangEngine.Event;

namespace KangEngine.Input.Base
{
    public abstract class JoystickBase : InputBase
    {
        protected bool isStatic = true;
        public float borderSize = 5.85f;
        protected Vector2 borderPosition = Vector2.zero;
        public bool smoothReturn = false;
        public float smoothFactor = 7f;
        private float xVel, yVel;

        public bool IsStatic
        {
            get { return isStatic; }
            set
            {
                if (isStatic == value) return;
                isStatic = value;
            }
        }

        internal override void ControlAwake()
        {
            base.ControlAwake();
        }


        internal override void UpdatePosition(Vector2 touchPos)
        {
            if (!enableAxisX && !enableAxisY)
                return;

            if (touchDown)
            {
                GetCurrentPosition(touchPos);

                currentDirection = currentPosition - defaultPosition;

                float currentDistance = Vector2.Distance(defaultPosition, currentPosition);
                float touchForce = 0f;

                float calculatedBorderSize = CalculateBorderSize();

                borderPosition = defaultPosition;
                borderPosition += currentDirection.normalized * calculatedBorderSize;

                if (currentDistance > calculatedBorderSize)
                {
                    currentPosition = borderPosition;
                    touchForce = 100f;
                }
                else touchForce = (currentDistance / calculatedBorderSize) * 100f;

                UpdateJoystickPosition();

                float asisX = currentDirection.normalized.x * touchForce / 100f * sensitivity;
                float asisY = currentDirection.normalized.y * touchForce / 100f * sensitivity;

                if (inverseAxisX) asisX = -asisX;
                if (inverseAxisY) asisY = -asisY;

                SetAxis(asisX, asisY);

                EventManager.Inst().DispatchEvent<object>(InputEvent.JOYSTICK_MOVE, inputData);
            }
            else
            {
                touchDown = true;
                if (!isStatic) UpdateTransparencyAndPosition(touchPos);

                EventManager.Inst().DispatchEvent<object>(InputEvent.JOYSTICK_START, inputData);
            }
        }

        protected abstract void GetCurrentPosition(Vector2 touchPos);

        protected abstract float CalculateBorderSize();

        protected abstract void UpdateJoystickPosition();

        protected abstract void UpdateTransparencyAndPosition(Vector2 touchPos);

        private System.Collections.IEnumerator SmoothReturnRun()
        {
            bool smoothReturnIsRun = true;

            while (smoothReturnIsRun && !touchDown && isStatic)
            {
                int dpMag = (int)defaultPosition.sqrMagnitude;
                int cpMag = (int)currentPosition.sqrMagnitude;

                currentPosition.x = Mathf.SmoothDamp(currentPosition.x, defaultPosition.x, ref xVel, smoothFactor * Time.smoothDeltaTime);
                currentPosition.y = Mathf.SmoothDamp(currentPosition.y, defaultPosition.y, ref yVel, smoothFactor * Time.smoothDeltaTime);

                if (cpMag == dpMag)
                {
                    currentPosition = defaultPosition;
                    smoothReturnIsRun = false;
                    xVel = yVel = 0f;
                }
                UpdateJoystickPosition();

                yield return null;
            }
        }

        internal override void ControlReset()
        {
            base.ControlReset();

            if (smoothReturn && isStatic)
            {
                StopCoroutine("SmoothReturnRun");
                StartCoroutine("SmoothReturnRun");
            }
            else
            {
                currentPosition = defaultPosition;
                UpdateJoystickPosition();
            }

            EventManager.Inst().DispatchEvent<object>(InputEvent.JOYSTICK_END, inputData);
        }
    }
}
