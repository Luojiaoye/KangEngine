using UnityEngine;
using KangEngine.Physics2.Interface;
using KangEngine.Event;
using KangEngine.Util;

namespace KangEngine.Physics2.Base
{
    public class MoveUnit : Physics2BaseUnit
    {
        private Vector3 _accelVelocity = Vector3.zero;
        private Vector3 _beginVelocity = Vector3.zero;
        private Vector3 _curVelocity = Vector3.zero;
        private Vector3 _maxVeloctiy = Vector3.one;
        private Vector3 _destPostion = Vector3.zero;
        private float _driftHeight;
        private float _driftSpeed = 0f;
        private Vector3 _targetPos;
        private bool _influence = false;
        private bool _beginDrift = false;
        private bool _beginThrow = false;

        public bool useDrift = false;
        public bool useDestination = false;

        private Vector3 _throwDir;

        public static uint MOVE_END = KangGUID.Build();
        public static uint THROW_END = KangGUID.Build();

        public MoveUnit(uint id, GameObject go, Vector3 beginVelocity, Vector3 accelVelocity) : base(id, go)
        {
            this._accelVelocity = accelVelocity;
            this._beginVelocity = beginVelocity;
        }
        public override void Start()
        {
            _influence = true;
            base.Start();
        }

        public override void Stop()
        {
            this.rigidbody.velocity = Vector3.zero;
            base.Stop();
        }

        public override void Pause()
        {
            this.status = false;
        }

        public override void FixedUpdate()
        {
            if (!this.status)
                return;

            if (useDestination)
            {
                if (Vector3.Distance(this._destPostion, this.transform.position) < 1f)
                {
                    this._curVelocity = Vector3.zero;
                    EventManager.Inst().DispatchEvent(THROW_END);
                    //this.Stop();
                }
                else
                    this._curVelocity += this._accelVelocity * Time.fixedDeltaTime;

                this.rigidbody.velocity = this._curVelocity;
                return;
            }

            if (this._influence)
            {
                if (!this._beginDrift && (this.transform.position.y <= this._destPostion.y))
                {
                    this._curVelocity += this._accelVelocity * Time.fixedDeltaTime;
                }
                else
                {
                    this._beginDrift = true;
                    this._curVelocity = Vector3.zero;
                }
                this.rigidbody.velocity = this._curVelocity;
            }

            if (this._beginDrift)
            {
                float speed = this._driftSpeed * Time.fixedDeltaTime;
                if (Mathf.Abs(this.transform.position.y - this._destPostion.y) < speed && this._influence)
                    _targetPos = new Vector3(this.transform.position.x, this._driftHeight + this._destPostion.y, this.transform.position.z);
                else if (Mathf.Abs(this.transform.position.y - (this._destPostion.y + this._driftHeight)) < speed)
                {
                    if (useDrift)
                        _targetPos = new Vector3(this.transform.position.x, this._destPostion.y, this.transform.position.z);
                    else
                    {
                        this._beginDrift = false;
                        this._influence = false;
                        this._beginThrow = true;
                        this.rigidbody.velocity = Vector3.zero;
                    }
                }

                if(!this._beginThrow)
                    this.transform.position = Vector3.MoveTowards(this.transform.position, this._targetPos, _driftSpeed * Time.fixedDeltaTime);
            }

            if (this._beginThrow)
            {
                EventManager.Inst().DispatchEvent(MOVE_END);
                if (this.rigidbody.velocity == Vector3.zero)
                {
                    float value = Random.Range(-1f, 1f);
                    float dirValue = value < 0 ? -1f : 1f;
                    _throwDir = new Vector3(dirValue * (1f + Mathf.Abs(value)), Mathf.Abs(value)+0.8f, Random.Range(-1f, 1f));
                    this.rigidbody.velocity = this._throwDir * 40f;
                }
                this.rigidbody.AddForce(Vector3.up * ( -40f), ForceMode.Force);
            }
        }

        public Vector3 maxVelocity { set { this._maxVeloctiy = value; } }

        public Vector3 destPosition { set { this._destPostion = value; } }

        public float heightLimit { set { this._driftHeight = value; } }

        public float driftSpeed { set { this._driftSpeed = value; } }
    }
}
