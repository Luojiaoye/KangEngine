using KangEngine.GameUnit.Interface;

namespace KangEngine.GameUnit.Base
{
    public class LifeMoveableGameUnit : MoveableGameUnit, ILifeGameUnit
    {
        private uint _hp;
        private uint _mp;
        private uint _sp;

        public uint HP
        {
            get { return this._hp; }
            set { this._hp = value; }
        }

        public uint MP
        {
            get { return this._mp; }
            set { this._mp = value; }
        }

        public uint SP
        {
            get { return this._sp; }
            set { this._sp = value; }
        }
    }
}
