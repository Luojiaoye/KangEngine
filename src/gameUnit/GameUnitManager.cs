using System;
using System.Collections.Generic;
using UnityEngine;
using KangEngine.Core;
using KangEngine.GameUnit.Interface;
using KangEngine.Render;
using KangEngine.Render.Base;
using KangEngine.Util;

namespace KangEngine.GameUnit
{
    public class GameUnitManager : KangSingleTon<GameUnitManager>
    {
        private Dictionary<GameObject, uint> _gameObjUnitDic;
        private Dictionary<uint, IGameUnit> _gameUnitDic;

        public GameUnitManager()
        {
            this._gameObjUnitDic = new Dictionary<GameObject, uint>();
            this._gameUnitDic = new Dictionary<uint, IGameUnit>();
        }

        public T CreateGameUnit<T>(uint gameUnitID, uint renderObjID) where T : class, IGameUnit, new()
        {
            RenderObject renderObj = KangSingleTon<RenderManager>.Inst().GetRenderObject<RenderObject>(renderObjID);
            if (renderObj == null)
                return default(T);

            renderObj.gameUnitID = gameUnitID;
            IGameUnit local = Activator.CreateInstance<T>();
            local.Init(gameUnitID, renderObj);
            return (local as T);
        }

        public void AddGameUnit(IGameUnit gameUnit)
        {
            if (gameUnit == null || _gameUnitDic.ContainsKey(gameUnit.gameUnitID))
                return;

            gameUnit.AddToPool(this._gameObjUnitDic);
            this._gameUnitDic[gameUnit.gameUnitID] = gameUnit;
        }

        public void RemoveGameUnit(uint gameUnitID)
        {
            IGameUnit unit = this.GetGameUnit<IGameUnit>(gameUnitID);
            if (unit == null)
                return;

            unit.RemoveFromPool(this._gameObjUnitDic);
            this._gameUnitDic.Remove(gameUnitID);
        }

        public void RemoveGameUnit(IGameUnit unit)
        {
            if (unit == null)
                return;

            this.RemoveGameUnit(unit.gameUnitID);
        }

        public IGameUnit GetGameUnit<T>(uint gameUnitID) where T : class, IGameUnit
        {
            IGameUnit unit = null;
            this._gameUnitDic.TryGetValue(gameUnitID, out unit);
            return (unit as T);
        }

        public IGameUnit GetGameUnit<T>(GameObject go) where T : class, IGameUnit
        {
            uint gameUnitID = KangGUID.invalidID;
            this._gameObjUnitDic.TryGetValue(go, out gameUnitID);
            return this.GetGameUnit<T>(gameUnitID);
        }

        public T GetGameUnitRenderObject<T>(uint gameUnitID) where T : RenderObject
        {
            IGameUnit unit = this.GetGameUnit<IGameUnit>(gameUnitID);
            if (unit == null)
                return default(T);

            return KangSingleTon<RenderManager>.Inst().GetRenderObject<T>(unit.renderObjID);
        }
    }
}
