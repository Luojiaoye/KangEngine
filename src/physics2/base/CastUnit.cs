using System;
using UnityEngine;
using KangEngine.Physics2.Interface;

namespace KangEngine.Physics2.Base
{
    public class CastUnit : Physics2BaseUnit
    {
        private float _force;
        private CastType _type;
        private Vector3 _destination;
        private Vector3 _direction;
        private bool _isContinueForce;
        private object _callbackObj;
        private Delegate _callback;
        private object[] _callbackArgs;

        public CastUnit(uint id, GameObject go, float force, CastType type = CastType.CT_Destination) : base(id, go)
        {
            this._direction = go.transform.forward;
            this._destination = go.transform.forward * 1.0f;
            this._force = force;
            this._type = type;
        }

        public override void Start()
        {
            this.rigidbody.isKinematic = false;

            if(this._type == CastType.CT_Direction)
                this.rigidbody.AddForce(this._direction * this._force, ForceMode.Force);

            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public void SetCallback(object callbackObj, Delegate callback, object[] callbackArgs)
        {
            this._callbackObj = callbackObj;
            this._callback = callback;
            this._callbackArgs = callbackArgs;
        }

        public Vector3 destination { get { return this._destination; } set { this._destination = value; } }
        public bool isContinueForce { get { return this._isContinueForce; } set { this._isContinueForce = value; } }
        public Vector3 direction { get { return this._direction; } set { this._direction = value; } }
        public float force { get { return this._force; } set { this._force = value; } }
    }

    public enum CastType
    {
        CT_Destination,
        CT_Direction,
    }
}
