using System;
using System.Collections.Generic;
using UnityEngine;
using KangEngine.Core;
using KangEngine.Physics2.Base;
using KangEngine.Physics2.Interface;

namespace KangEngine.Physics2
{
    public class Physics2ObjectManager : KangSingleTon<Physics2ObjectManager>
    {
        private Dictionary<uint, IPhysics2> _physics2Dic;

        public Physics2ObjectManager()
        {
            this._physics2Dic = new Dictionary<uint, IPhysics2>();
        }

        public SpinUnit CreateSpinObject(uint id, GameObject go, SpinType type = SpinType.ST_Normal)
        {
            IPhysics2 spin;
            this._physics2Dic.TryGetValue(id, out spin);
            if (spin != null)
                return (SpinUnit)spin;

            SpinUnit spinObj = new SpinUnit(id, go, type);
            this._physics2Dic[id] = spinObj;
            return spinObj;
        }

        public CastUnit CreateCastObject(uint id, GameObject go, float force, CastType type = CastType.CT_Destination)
        {
            IPhysics2 cast;
            this._physics2Dic.TryGetValue(id, out cast);
            if (cast != null)
                return (CastUnit)cast;

            CastUnit castObj = new CastUnit(id, go, force, type);
            this._physics2Dic[id] = castObj;
            return castObj;
        }

        public MoveUnit CreateMoveUnit(uint id, GameObject go, Vector3 beginVeloctiy, Vector3 accelVeloctiy)
        {
            IPhysics2 move;
            this._physics2Dic.TryGetValue(id, out move);
            if (move != null)
                return (MoveUnit)move;

            MoveUnit moveObj = new MoveUnit(id, go, beginVeloctiy, accelVeloctiy);
            this._physics2Dic[id] = moveObj;
            return moveObj;
        }

        public void AddPhysics2Object(IPhysics2 physics2Obj)
        {
            if (physics2Obj == null)
                return;

            if (!this._physics2Dic.ContainsKey(physics2Obj.id))
                this._physics2Dic[physics2Obj.id] = physics2Obj;
        }

        public void RemovePhysics2Object(uint id)
        {
            if (this._physics2Dic.ContainsKey(id))
                this._physics2Dic.Remove(id);
        }

        private IPhysics2 GetPhysics2(uint id)
        {
            IPhysics2 physics;
            this._physics2Dic.TryGetValue(id, out physics);
            return physics;
        }

        public void RemovePhysics2Object(IPhysics2 physics2Obj)
        {
            if (physics2Obj == null)
                return;

            this.RemovePhysics2Object(physics2Obj.id);
        }

        public void FixedUpdate()
        {
            List<uint> keys = new List<uint>(_physics2Dic.Keys);
            for (int idx = 0; idx < keys.Count; idx++)
            {
                IPhysics2 physics2;
                if (_physics2Dic.TryGetValue(keys[idx], out physics2))
                    physics2.FixedUpdate();
            }
        }
    }
}
