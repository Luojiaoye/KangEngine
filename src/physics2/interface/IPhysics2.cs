using UnityEngine;

namespace KangEngine.Physics2.Interface
{
    public interface IPhysics2
    {
        void FixedUpdate();

        void Start();

        void Stop();

        void Pause();

        uint id { get; }

        Rigidbody rigidbody { get; }
        Transform transform { get; }
    }
}
