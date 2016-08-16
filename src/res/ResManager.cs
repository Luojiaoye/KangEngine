using KangEngine.Core;
using UnityEngine;
using KangEngine.Res.Base;

namespace KangEngine.Res
{
    public class ResManager : KangSingleTon<ResManager>
    {
        public ResManager() { }

        public T LoadAsset<T>(ResType type, string path) where T : Object
        {
            return Resources.Load<T>(ResConfig.GetFullPath(type, path));
        }

        public T[] LoadAssets<T>(ResType type, string path) where T : Object
        {
            return Resources.LoadAll<T>(ResConfig.GetFullPath(type, path));
        }
    }
}
