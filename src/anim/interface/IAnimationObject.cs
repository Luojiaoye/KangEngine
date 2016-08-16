
using System;

namespace KangEngine.Animation.Interface
{
    public interface IAnimationObject
    {
        void RemoveEvents();
        void ForceDispatchEvent(uint eventNameHash);
        //AnimationData GetAnimationData(uint hashName);
        bool GetBool(string name);
        bool GetBool(uint id);
        float GetFloat(string name);
        float GetFloat(uint id);
        int GetInteger(string name);
        int GetInteger(uint id);
        void Play(string stateName);
        void Play(uint stateNameHash);
        void ResetTrigger(string name);
        void ResetTrigger(uint id);
        void SetBool(string name, bool value);
        void SetBool(uint id, bool value);
        void SetEvent(object thisObj, Action<uint, object> callback = null, params object[] args);
        void SetEvent<T1>(object thisObj, Action<uint, object, T1> callback = null, params object[] args);
        void SetEvent<T1, T2>(object thisObj, Action<uint, object, T1, T2> callback = null, params object[] args);
        void SetFloat(string name, float value);
        void SetFloat(uint id, float value);
        void SetFloat(string name, float value, float dampTime, float deltaTime);
        void SetFloat(uint id, float value, float dampTime, float deltaTime);
        void SetInteger(string name, int value);
        void SetInteger(uint id, int value);
        void SetTrigger(string name);
        void SetTrigger(uint id);

        //AnimationConfig animCfg { set; }
        uint id { get; }
        object owner { get; }
        float speed { get; set; }

    }
}
