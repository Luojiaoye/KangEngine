using UnityEngine;
namespace KangEngine.Core
{
    public class KangComponent : MonoBehaviour
    {
        private uint _id;

        public uint id { get { return this._id; } set { this._id = value; } }
    }
}
