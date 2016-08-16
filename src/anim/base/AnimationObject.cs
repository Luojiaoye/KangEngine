using System;
using UnityEngine;
using KangEngine.Animation.Interface;

namespace KangEngine.Animation.Base
{
    public class AnimationObject : IAnimationObject
    {
        private Animator _owner;
        private uint _id;

        public AnimationObject( uint objID, Animator owner )
        {
            this._id = objID;
            this._owner = owner;

            if (this._owner == null)
                throw new ArgumentNullException("KangEngine: Animation object constructor param animator is null.");
        }
        public object owner
        {
            get { return _owner; }
        }

        public uint id
        {
            get { return _id; }
        }

        public float speed
        {
            get { return _owner.speed; }
            set { _owner.speed = value; }
        }

        public void RemoveEvents()
        {

        }

        public void ForceDispatchEvent(uint eventNameHash)
        {

        }

        public bool GetBool(string name)
        {
            return _owner.GetBool( name );
        }
        public bool GetBool(uint id)
        {
            return _owner.GetBool( (int)id );
        }

        public float GetFloat(string name)
        {
            return this._owner.GetFloat(name);
        }

        public float GetFloat(uint id)
        {
            return this._owner.GetFloat((int)id);
        }

        public int GetInteger(string name)
        {
            return this._owner.GetInteger(name);
        }

        public int GetInteger(uint id)
        {
            return this._owner.GetInteger((int)id);
        }

        public void Play(string stateName)
        {
            this._owner.Play(stateName);
        }

        public void Play(uint stateNameHash)
        {
            this._owner.Play((int)stateNameHash);
        }

        public void ResetTrigger(string name)
        {
            this._owner.ResetTrigger(name);
        }

        public void ResetTrigger(uint id)
        {
            this._owner.ResetTrigger((int)id);
        }

        public void SetBool(string name, bool value)
        {
            this._owner.SetBool(name, value);
        }

        public void SetBool(uint id, bool value)
        {
            this._owner.SetBool((int)id, value);
        }

        public void SetEvent(object thisObj, Action<uint, object> callback = null, params object[] args)
        {
            //KangSingleton<KangAnimationEventManager>.Inst().AddEventObject((int)this._id, this._owner, thisObj, callback, args);
        }

        public void SetEvent<T1>(object thisObj, Action<uint, object, T1> callback = null, params object[] args)
        {
            //KangSingleton<KangAnimationEventManager>.Inst().AddEventObject((int)this._id, this._owner, thisObj, callback, args);
        }

        public void SetEvent<T1, T2>(object thisObj, Action<uint, object, T1, T2> callback = null, params object[] args)
        {
            //KangSingleton<KangAnimationEventManager>.Inst().AddEventObject((int)this._id, this._owner, thisObj, callback, args);
        }

        public void SetFloat(string name, float value)
        {
            this._owner.SetFloat(name, value);
        }

        public void SetFloat(uint id, float value)
        {
            this._owner.SetFloat((int)id, value);
        }

        public void SetFloat(string name, float value, float dampTime, float deltaTime)
        {
            this._owner.SetFloat(name, value, dampTime, deltaTime);
        }

        public void SetFloat(uint id, float value, float dampTime, float deltaTime)
        {
            this._owner.SetFloat((int)id, value, dampTime, deltaTime);
        }

        public void SetInteger(string name, int value)
        {
            this._owner.SetInteger(name, value);
        }

        public void SetInteger(uint id, int value)
        {
            this._owner.SetInteger((int)id, value);
        }

        public void SetTrigger(string name)
        {
            this._owner.SetTrigger(name);
        }

        public void SetTrigger(uint id)
        {
            this._owner.SetTrigger((int)id);
        }
    }
}
