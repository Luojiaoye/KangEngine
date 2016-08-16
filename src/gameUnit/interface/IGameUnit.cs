using System.Collections.Generic;
using UnityEngine;
using KangEngine.Render.Base;

namespace KangEngine.GameUnit.Interface
{
    public interface IGameUnit
    {
        uint gameUnitID { get; }
        uint renderObjID { get; }
        Vector3 position { get; set; }
        Transform transform { get; }

        void Init( uint gameUnitID, RenderObject renderObj );
        void AddComponent<T>() where T : Component;
        T GetComponent<T>() where T : Component;
        T GetComponentInChildren<T>() where T : Component;
        T GetComponentInParent<T>() where T : Component;
        void AddToPool(Dictionary<GameObject, uint> pool);
        void RemoveFromPool(Dictionary<GameObject, uint> pool);
    }
}
