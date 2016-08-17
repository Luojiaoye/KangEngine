using UnityEngine;
using KangEngine.Core;

namespace KangEngine.Input.Base
{
    public struct InputData
    {
        internal string name;
        internal float axisX;
        internal float axisY;
    }

    public abstract class InputBase : KangComponent
    {
        protected InputData inputData;
        public float sensitivity = 4f;
        protected bool showTouchZone = true;
        private string _inputName = "Joystick";
        public bool enableAxisX = true;
        public bool inverseAxisX = false;
        public bool enableAxisY = true;
        public bool inverseAxisY = false;

        internal int touchId = -1;
        internal bool touchDown = false;

        protected Vector2 defaultPosition, currentPosition, currentDirection;

        internal float AxisValueX { get; private set; }
        internal float AxisValueY { get; private set; }

        private void Awake()
        {
            ControlAwake();
            InputManager.Inst().AddInput(this._inputName, this);
        }

        public bool ShowTouchZone
        {
            get { return showTouchZone; }
            set
            {
                if (showTouchZone == value) return;
                showTouchZone = value;
                ShowingTouchZone();
            }
        }

        protected virtual void ShowingTouchZone() { }

        public string inputName
        {
            get { return this._inputName; }
            set
            {
                if (this._inputName == value || value == string.Empty) return;
                this._inputName = value;
                inputData.name = this._inputName;
                gameObject.name = this._inputName;
            }
        }


        internal virtual void ControlAwake()
        {
            inputData.name = this._inputName;
        }

        internal virtual void ControlDisable() { }

        internal virtual void CalculationSizeAndPosition() { }

        internal virtual bool CheckTouchPosition(Vector2 touchPos) { return false; }

        internal abstract void UpdatePosition(Vector2 touchPos);

        internal virtual void ControlReset()
        {
            touchId = -1;
            touchDown = false;
            SetAxis(0f, 0f);
        }

        protected void SetAxis(float x, float y)
        {
            AxisValueX = x;
            AxisValueY = y;
            inputData.axisX = x;
            inputData.axisY = y;
        }
    }
}
