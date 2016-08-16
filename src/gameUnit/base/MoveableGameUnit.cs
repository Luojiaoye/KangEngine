using System.Collections.Generic;
using KangEngine.GameUnit.Interface;

namespace KangEngine.GameUnit.Base
{
    public class MoveableGameUnit : GameUnitBase, IMoveableGameUnit
    {
        private float _curMoveSpeed;
        private Dictionary<object, float> _speedDic = new Dictionary<object, float>();

        public void SetSpeed<T>(T key, float speed)
        {
            this._speedDic[key] = speed;
        }

        public float GetSpeed<T>(T key)
        {
            return this._speedDic[key];
        }

        public void SetCurrentSpeed<T>(T key)
        {
            float naN = float.NaN;
            this._speedDic.TryGetValue( key, out naN );
            this._curMoveSpeed = naN == float.NaN ? 0f : naN;
        }

        public float moveSpeed
        {
            get { return this._curMoveSpeed; }
        }
    }
}
