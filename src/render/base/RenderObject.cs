using UnityEngine;

namespace KangEngine.Render.Base
{
    public class RenderObject
    {
        private uint _id;
        private uint _gameUnitID;
        private GameObject _refGameObj;

        public uint id
        {
            get { return _id; }
        }

        public uint gameUnitID
        {
            get { return _gameUnitID; }
            set { _gameUnitID = value; }
        }

        public GameObject refGameObj
        {
            get { return _refGameObj; }
        }

        public void Init( uint objID, GameObject gameObj )
        {
            _id = objID;
            _refGameObj = gameObj;
        }
        public virtual void Destroy()
        {
            _id = 0;
            Object.Destroy( _refGameObj );
            _refGameObj = null;
        }
    }
}
