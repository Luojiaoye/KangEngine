using UnityEngine;
using KangEngine.Physics2.Interface;

namespace KangEngine.Physics2.Base
{
    public class Physics2BaseUnit : IPhysics2
    {
        private uint _id;
        private Rigidbody _rigidbody;
        private GameObject _gameObject;
        protected bool status = false;

        public Physics2BaseUnit(uint id, GameObject go)
        {
            this._id = id;
            this._gameObject = go;
            this._rigidbody = this._gameObject.GetComponent<Rigidbody>();
            if (this._rigidbody == null)
            {
                this._rigidbody = this._gameObject.AddComponent<Rigidbody>();
                this.rigidbody.useGravity = false;
                this.rigidbody.isKinematic = false;
            }
        }

        public virtual void Start()
        {
            status = true;
            Physics2ObjectManager.Inst().AddPhysics2Object(this);
        }

        public virtual void Stop()
        {
            status = false;
            Physics2ObjectManager.Inst().RemovePhysics2Object(this.id);
        }

        public virtual void Pause() { status = false; }

        public virtual void FixedUpdate() { }

        public uint id { get { return this._id; } }
        public Transform transform { get { return this._gameObject.transform; } }
        public Rigidbody rigidbody { get { return this._rigidbody; } }
    }
}
