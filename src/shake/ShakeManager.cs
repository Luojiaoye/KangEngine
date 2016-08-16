using System.Collections.Generic;
using UnityEngine;
using KangEngine.Core;
using KangEngine.Shake.Base;
using KangEngine.Util;

namespace KangEngine.Shake
{
    public class ShakeManager : KangSingleTon<ShakeManager>
    {
        private Dictionary<uint, ShakeObject> _shakes;
        private Vector3 _addDeltaPosition;
        private Vector3 _addDeltaRotation;

        public ShakeManager()
        {
            this._shakes = new Dictionary<uint, ShakeObject>();
        }

        public ShakeObject CreateShakeObject(uint id, ShakeData shakeData)
        {
            ShakeObject shakeObj = null;
            this._shakes.TryGetValue(id, out shakeObj);
            if (shakeObj == null)
            {
                shakeObj = new ShakeObject(id, shakeData);
                this._shakes[id] = shakeObj;
            }

            return shakeObj;
        }

        public ShakeObject CreateShakeObject(uint id, float magintude, float roughtness)
        {
            ShakeObject shakeObj = null;
            this._shakes.TryGetValue(id, out shakeObj);
            if (shakeObj == null)
            {
                shakeObj = new ShakeObject(id, magintude, roughtness);
                this._shakes[id] = shakeObj;
            }

            return shakeObj;
        }

        public ShakeObject CreateShakeObject(uint id, float magintude, float roughtness, float fadeInTime, float fadeOutTime)
        {
            ShakeObject shakeObj = null;
            this._shakes.TryGetValue(id, out shakeObj);
            if (shakeObj == null)
            {
                shakeObj = new ShakeObject(id, magintude, roughtness, fadeInTime, fadeOutTime);
                this._shakes[id] = shakeObj;
            }

            return shakeObj;
        }

        public void AddShakedTransform(uint shakeID, Transform trans)
        {
            ShakeObject shakeObj = GetShakeObject(shakeID);
            if (shakeObj == null)
                return;

            shakeObj.BindTransform(trans);
        }

        public ShakeObject GetShakeObject(uint id)
        {
            ShakeObject shakeObj = null;
            this._shakes.TryGetValue(id, out shakeObj);
            return shakeObj;
        }

        public void RemoveShakeObject(uint id)
        {
            ShakeObject shakeObj = GetShakeObject(id);
            if (shakeObj != null)
            {
                shakeObj.ResetTransform();
                this._shakes.Remove(id);
                shakeObj = null;
            }
        }

        public void Update()
        {
            this._addDeltaPosition = Vector3.zero;
            this._addDeltaRotation = Vector3.zero;
            ShakeObject shakeObj = null;
            foreach (KeyValuePair<uint, ShakeObject> pair in this._shakes)
            {
                shakeObj = pair.Value;
                if (shakeObj == null)
                    continue;

                switch (shakeObj.state)
                {
                    case ShakeState.Inactive:
                        shakeObj.ResetTransform();
                        RemoveShakeObject(shakeObj.id);
                        break;
                    case ShakeState.FadingIn:
                    case ShakeState.FadingOut:
                    case ShakeState.Persistent:
                        if (shakeObj.bindTransform == null)
                            return;
                        this._addDeltaPosition += KangVector.MultiplyVectors(shakeObj.UpdateShake(), shakeObj.postionInfluence);
                        this._addDeltaRotation += KangVector.MultiplyVectors(shakeObj.UpdateShake(), shakeObj.rotationInfluence);
                        shakeObj.bindTransform.position += this._addDeltaPosition;
                        Vector3 angle = shakeObj.bindTransform.eulerAngles;
                        angle.x += this._addDeltaRotation.x;
                        angle.y += this._addDeltaRotation.y;
                        angle.z += this._addDeltaRotation.z;
                        shakeObj.bindTransform.rotation = Quaternion.Euler(angle);
                        break;
                }
            }
        }
    }
}
