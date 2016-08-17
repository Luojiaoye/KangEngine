using System;
using System.Reflection;
using UnityEngine;
using KangEngine.Camera;
using KangEngine.Shake;
using KangEngine.Physics2;
using KangEngine.Timer;
using KangEngine.Input;

namespace KangEngine.Core
{
    public sealed class KangEngineRoot : KangComponent
    {
        private static KangEngineRoot _inst;
        private static object _gameEntry;
        private MethodInfo _awakeMethod;
        private MethodInfo _startMethod;
        private MethodInfo _updateMethod;
        private MethodInfo _fixedUpdateMethod;
        private MethodInfo _lateUpdateMethod;
        private MethodInfo _guiMethod;

        public KangEngineRoot()
        {
        }

        private void Awake()
        {
            if (_inst == null)
                _inst = this;

            if (_gameEntry == null)
                this.Init();

            if (this._awakeMethod != null)
                this._awakeMethod.Invoke(_gameEntry, null);
        }

        private void Start()
        {
            if (this._startMethod != null)
                this._startMethod.Invoke(_gameEntry, null);
        }

        private void Update()
        {
            KangSingleTon<KangCameraManager>.Inst().Update();
            ShakeManager.Inst().Update();
            TimerManager.Inst().Update();
            InputManager.Inst().Update();

            if (this._updateMethod != null)
                this._updateMethod.Invoke(_gameEntry, null);
        }

        private void FixedUpdate()
        {
            KangSingleTon<KangCameraManager>.Inst().FixedUpdate();
            Physics2ObjectManager.Inst().FixedUpdate();
            if (this._fixedUpdateMethod != null)
                this._fixedUpdateMethod.Invoke(_gameEntry, null);
        }

        private void LateUpdate()
        {
            if (this._lateUpdateMethod != null)
                this._lateUpdateMethod.Invoke(_gameEntry, null);
        }

        private void OnGUI()
        {
            if (this._guiMethod != null)
                this._guiMethod.Invoke(_gameEntry, null);
        }

        private void Init()
        {
            Type typeObj = Type.GetType("Kang.Root.Entry.GameEntry,GameRuntime");
            if (typeObj == null)
                typeObj = Type.GetType("Kang.Root.Entry.GameEntry,Assembly-CSharp");

            if (typeObj == null)
            {
                Array assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    typeObj = assembly.GetType("Kang.Root.Entry.GameEntyr");
                    if (typeObj != null)
                        break;
                }
            }

            if (typeObj != null)
            {
                _gameEntry = typeObj.Assembly.CreateInstance(typeObj.FullName);
                this._awakeMethod = this.ReflectMethod(typeObj, "Awake");
                this._startMethod = this.ReflectMethod(typeObj, "Start");
                this._updateMethod = this.ReflectMethod(typeObj, "Update");
                this._fixedUpdateMethod = this.ReflectMethod(typeObj, "FixedUpdate");
                this._lateUpdateMethod = this.ReflectMethod(typeObj, "LateUpdate");
                this._guiMethod = this.ReflectMethod(typeObj, "OnGUI");
            }
            else
            {
                Debug.Log("Not Find GameRuntime.dll");
            }
        }

        private MethodInfo ReflectMethod(Type typeObj, string methodName)
        {
            return typeObj.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
