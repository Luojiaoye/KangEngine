using UnityEngine;

namespace KangEngine.Shake.Base
{
    public struct ShakeData
    {
        public Vector3 positionInfluence;
        public Vector3 rotationInfluence;
        public float magnitude;
        public float roughness;

        public float fadeInTime;
        public float fadeOutTime;

        public float deltaRough;
        public float deltaMagitude;
    }

    public enum ShakeState
    {
        FadingIn,
        FadingOut,
        Persistent,
        Inactive,
    }
}
