using System;
using System.Collections.Generic;
using UnityEngine;
using KangEngine.Render.Base;
using KangEngine.GameUnit.Interface;

namespace KangEngine.GameUnit.Base
{
    public abstract class GameUnitBase : IGameUnit
    {
        private uint _gameUnitID;
        private GameObject _refGameObj;
        private RenderObject _renderObj;

        public uint gameUnitID
        {
            get { return _gameUnitID; }
        }

        public uint renderObjID
        {
            get { return _renderObj.id; }
        }

        public Vector3 position
        {
            get { return this._refGameObj.transform.position; }
            set { this._refGameObj.transform.position = value; }
        }

        public Transform transform
        {
            get { return this._refGameObj.transform; }
        }

        public virtual void Init(uint gameUnitID, RenderObject renderObj)
        {
            this._gameUnitID = gameUnitID;
            if (renderObj == null)
                throw new ArgumentNullException("KangEngine: Game Unit Init() param render object is null.");

            this._renderObj = renderObj;
            this._refGameObj = this._renderObj.refGameObj;
            if (this._refGameObj == null)
                throw new NullReferenceException("KangEngine: Game Unit Init() refer game object of render object is null.");

            this._refGameObj.name = this._refGameObj.name + string.Format("[{0}]", this._gameUnitID);
        }

        public void AddToPool(Dictionary<GameObject, uint> pool)
        {
            if (pool == null || pool.ContainsKey(this._refGameObj))
                return;

            pool[this._refGameObj] = this._gameUnitID;
        }

        public void RemoveFromPool(Dictionary<GameObject, uint> pool)
        {
            if (pool == null || !pool.ContainsKey(this._refGameObj))
                return;

            pool.Remove(this._refGameObj);
        }

        public void AddComponent<T>() where T : Component
        {
            this._refGameObj.AddComponent<T>();
        }

        public T GetComponent<T>() where T : Component
        {
            return this._refGameObj.GetComponent<T>();
        }

        public T GetComponentInChildren<T>() where T : Component
        {
            return this._refGameObj.GetComponentInChildren<T>();
        }

        public T GetComponentInParent<T>() where T : Component
        {
            return this._refGameObj.GetComponentInParent<T>();
        }
    }
}
