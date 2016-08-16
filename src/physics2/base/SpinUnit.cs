using UnityEngine;
using KangEngine.Physics2;
using KangEngine.Physics2.Interface;

namespace KangEngine.Physics2.Base
{
    public class SpinUnit : Physics2BaseUnit
    {
        private Vector3 _center;
        private float _spinSpeed;
        private Vector2 _randomRange;
        private SpinType _type;
        private Vector3 _axis;
        private bool _selfSpin = false;

        public bool useRotate = false;
        private float _radius;
        private float _angular;

        public SpinUnit(uint id, GameObject go, SpinType type = SpinType.ST_Normal) : base(id, go)
        {
            this._type = type;

            this._radius = 0f;
            this._center = this.transform.position;
            this._spinSpeed = 60.0f;
            this._axis = Vector3.up;
            this._randomRange = new Vector2(10f, 60f);
        }

        public override void FixedUpdate()
        {
            if (!this.status)
                return;

            float speed = this._type == SpinType.ST_Random ? Random.Range(this._randomRange.x, this._randomRange.y) : this._spinSpeed;
            if (this._selfSpin)
                this.transform.Rotate(this._axis * spinSpeed, Space.World);
            else
            {
                if (this.useRotate)
                    this.transform.RotateAround(this._center, this._axis, speed * Time.fixedDeltaTime);
                else if(this._axis == Vector3.up)
                {
                    this._angular += speed * Time.fixedDeltaTime;
                    this.transform.position = new Vector3(this.center.x + Mathf.Cos(this._angular) * this._radius, this.transform.position.y, this.center.z + Mathf.Sin(this._angular)*this._radius);
                }
            }
        }

        public override void Start()
        {
            if (this._radius != 0f && Vector3.Distance(this._center, this.transform.position) > 0.5f)
            {
                Vector3 dir = (this.transform.position - this._center).normalized;
                Vector3 targetPos = this._center + dir * _radius;
                this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 100f * Time.fixedDeltaTime);
            }
            base.Start();            
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Pause()
        {
            this.status = false;
        }

        public Vector3 center { get { return this._center; } set { this._center = value; } }
        public Vector3 axis { get { return this._axis; } set { this._axis = value; } }
        public Vector2 randomRange { set { this._randomRange = value; } }
        public float spinSpeed { get { return this._spinSpeed; } set { this._spinSpeed = value; } }
        public bool selfSpin { get { return this._selfSpin; } set { this._selfSpin = value; } }
        public float radius { set { this._radius = value; } }
    }

    public enum SpinType
    {
        ST_Normal,
        ST_Random,
    }
}
