namespace KangEngine.GameUnit.Base
{
    public class StaticGameUnit : GameUnitBase
    {
        private uint _hp;

        public uint HP
        {
            get { return this._hp; }
            set { this._hp = value; }
        }
    }
}
