using UnityEngine;

namespace KangEngine.Shake.Base
{
    public class ShakeObject
    {
        private uint _id;
        private ShakeData _shakeData;
        private float _curFadeTime;
        private bool _persistent = false;
        private float _tick = 0;
        private Vector3 _sum;
        private Vector3 _originalPos;
        private Quaternion _originalRotation;
        private Transform _trans;

        public static ShakeData DEFAULT_SHAKE_DATA = new ShakeData
        {
            positionInfluence = new Vector3(0.15f, 0.15f, 0.15f),
            rotationInfluence = Vector3.one,
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 0.0f,
            magnitude = 0.0f,
            fadeOutTime = 0.0f,
            roughness = 0.0f
        };

        public static ShakeData DUMP_SHAKE_DATA = new ShakeData
        {
            positionInfluence = Vector3.one * 0.15f,
            rotationInfluence = Vector3.one,
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 0.1f,
            magnitude = 2.5f,
            fadeOutTime = 0.75f,
            roughness = 4.0f
        };

        public static ShakeData EXPLOSION_SHAKE_DATA = new ShakeData
        {
            positionInfluence = Vector3.one * 0.25f,
            rotationInfluence = new Vector3(4, 1, 1),
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 0f,
            magnitude = 5f,
            fadeOutTime = 1.5f,
            roughness = 10f
        };

        public static ShakeData EARTHQUAKE_SHAKE_DATA = new ShakeData
        {
            positionInfluence = Vector3.one * 0.25f,
            rotationInfluence = new Vector3(1, 1, 4),
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 2f,
            magnitude = 0.6f,
            fadeOutTime = 10f,
            roughness = 3.5f
        };

        public static ShakeData HANDHELD_SHAKE_DATA = new ShakeData
        {
            positionInfluence = Vector3.zero,
            rotationInfluence = new Vector3(1, 0.5f, 0.5f),
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 5f,
            magnitude = 1f,
            fadeOutTime = 10f,
            roughness = 0.25f
        };

        public static ShakeData BADTRIP_SHAKE_DATA = new ShakeData
        {
            positionInfluence = new Vector3(0, 0, 0.15f),
            rotationInfluence = new Vector3(2, 1, 4),
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 5f,
            magnitude = 10f,
            fadeOutTime = 10f,
            roughness = 0.15f
        };

        public static ShakeData VIBRATION_SHAKE_DATA = new ShakeData
        {
            positionInfluence = new Vector3(0, 0.15f, 0),
            rotationInfluence = new Vector3(1.25f, 0, 4),
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 2f,
            magnitude = 0.4f,
            fadeOutTime = 2f,
            roughness = 20f
        };

        public static ShakeData ROUGHDRIVING_SHAKE_DATA = new ShakeData
        {
            positionInfluence = Vector3.zero,
            rotationInfluence = Vector3.one,
            deltaMagitude = 1.0f,
            deltaRough = 1.0f,
            fadeInTime = 1f,
            magnitude = 1f,
            fadeOutTime = 1f,
            roughness = 2f
        };

        public ShakeObject(uint id, ShakeData shakeData)
        {
            this._shakeData = shakeData;

            if (this._shakeData.fadeInTime > 0)
            {
                this._curFadeTime = 0f;
                this._persistent = true;
            }
            else
            {
                this._persistent = false;
                this._curFadeTime = 1;
            }
            this._tick = Random.Range(100f, 100f);
        }

        public ShakeObject(uint id, float magnitude, float roughtness)
        {
            this._id = id;
            this._shakeData = DEFAULT_SHAKE_DATA;
            this._shakeData.magnitude = magnitude;
            this._shakeData.roughness = roughtness;

            this._tick = Random.Range(100f, 100f);
            this._persistent = true;
        }

        public ShakeObject(uint id, float magnitude, float roughtness, float fadeInTime, float fadeOutTime)
        {
            this._id = id;
            this._shakeData = DEFAULT_SHAKE_DATA;
            this._shakeData.magnitude = magnitude;
            this._shakeData.roughness = roughtness;
            this._shakeData.fadeOutTime = fadeOutTime;

            if (fadeInTime > 0)
            {
                this._shakeData.fadeInTime = fadeInTime;
                this._curFadeTime = 0f;
                this._persistent = true;
            }
            else
            {
                this._persistent = false;
                this._curFadeTime = 1;
            }
            this._tick = Random.Range(100f, 100f);
        }

        public Vector3 UpdateShake()
        {
            this._sum.x = Mathf.PerlinNoise(this._tick, 0) - 0.5f;
            this._sum.y = Mathf.PerlinNoise(0, this._tick) - 0.5f;
            this._sum.z = Mathf.PerlinNoise(this._tick, this._tick) - 0.5f;
            if (this._shakeData.fadeInTime > 0 && this._persistent)
            {
                if (this._curFadeTime < 1)
                    this._curFadeTime += Time.deltaTime / this._shakeData.fadeInTime;
                else if (this._shakeData.fadeOutTime > 0)
                    this._persistent = false;
            }

            if (!this._persistent)
                this._curFadeTime -= Time.deltaTime / this._shakeData.fadeOutTime;

            if (this._persistent)
                this._tick += Time.deltaTime * this._shakeData.roughness * this._shakeData.deltaRough;
            else
                this._tick += Time.deltaTime * this._shakeData.roughness * this._shakeData.deltaRough * this._curFadeTime;
            return this._sum * this._shakeData.magnitude * this._shakeData.deltaMagitude * this._curFadeTime;
        }

        public void StartFadeIn(float fadeInTime)
        {
            if (fadeInTime == 0f)
                this._curFadeTime = 1.0f;

            this._shakeData.fadeInTime = fadeInTime;
            this._shakeData.fadeOutTime = 0f;
            this._persistent = true;
        }

        public void StartFadeOut(float fadeOutTime)
        {
            if (fadeOutTime == 0f)
                this._curFadeTime = 0f;

            this._shakeData.fadeOutTime = fadeOutTime;
            this._shakeData.fadeInTime = 0f;
            this._persistent = false;
        }

        public void BindTransform(Transform trans)
        {
            this._trans = trans;
            this._originalPos = new Vector3(trans.position.x, trans.position.y, trans.position.z);
            this._originalRotation = trans.rotation;
        }

        public void ResetTransform()
        {
            this._trans.position = this._originalPos;
            this._trans.rotation = this._originalRotation;
        }

        public uint id { get { return this._id; } }
        public float NormalFadeTime
        {
            get { return this._curFadeTime; }
        }

        public bool IsShaking { get { return (this._curFadeTime > 0 && this._persistent); } }

        public bool IsFadingIn { get { return this._shakeData.fadeInTime > 0 && this._persistent && this._curFadeTime < 1f; } }

        public bool IsFadingOut { get { return this._curFadeTime > 0 && !this._persistent; } }

        public ShakeState state
        {
            get
            {
                if (IsShaking)
                    return ShakeState.Persistent;
                else if (IsFadingIn)
                    return ShakeState.FadingIn;
                else if (IsFadingOut)
                    return ShakeState.FadingOut;
                else
                    return ShakeState.Inactive;
            }
        }

        public Vector3 postionInfluence { get { return this._shakeData.positionInfluence; } }

        public Vector3 rotationInfluence { get { return this._shakeData.rotationInfluence; } }

        public Transform bindTransform { get { return this._trans; } }
    }
}
