using System.Collections.Generic;
using UnityEngine;
using KangEngine.Core;
using KangEngine.Render.Base;
using KangEngine.Res;
using KangEngine.Res.Base;
using KangEngine.Util;
using KangEngine.Animation.Interface;
using KangEngine.Animation;

namespace KangEngine.Render
{
    public class RenderManager : KangSingleTon<RenderManager>
    {
        private Dictionary<uint, RenderObject> _renderObjDic;
        private Dictionary<uint, TransformObject> _transObjDic;
        private LinkedList<uint> _completeTransObjList;

        public RenderManager()
        {
            _renderObjDic = new Dictionary<uint, RenderObject>();
            _transObjDic = new Dictionary<uint, TransformObject>();
            _completeTransObjList = new LinkedList<uint>();
        }

        public AnimRenderObject CreateAnimaRenderObject(string path)
        {
            AnimRenderObject animRenderObj = CreateRenderObject<AnimRenderObject>(path);
            if (animRenderObj == null)
                return animRenderObj;

            IAnimationObject animationObj = KangSingleTon<AnimationManager>.Inst().CreateAnimationObject(animRenderObj.refGameObj);
            if (animationObj != null)
                animRenderObj.animationObj = animationObj;

            KangSingleTon<AnimationManager>.Inst().AddAnimationObject(animationObj);
            return animRenderObj;
        }

        public StaticRenderObject CreateStaticRenderObject(string path)
        {
            StaticRenderObject sro = CreateRenderObject<StaticRenderObject>(path);
            if (sro == null)
                return sro;

            return sro;
        }

        public StaticRenderObject CreateStaticRenderObject(GameObject go)
        {
            StaticRenderObject sro = CreateRenderObject<StaticRenderObject>(go);
            if (sro == null)
                return sro;

            return sro;
        }


        public T GetRenderObject<T>(uint id) where T : RenderObject
        {
            if (!_renderObjDic.ContainsKey(id))
                return default(T);

            return (_renderObjDic[id] as T);
        }

        public void RemoveRenderObject(uint id)
        {
            if (!this._renderObjDic.ContainsKey(id))
                return;

            RenderObject ro = null;
            this._renderObjDic.TryGetValue(id, out ro);
            if (ro == null)
                return;

            _renderObjDic.Remove(id);
            ro.Destroy();
        }

        public void RemoveRenderObject(RenderObject ro)
        {
            if (ro == null)
                return;

            RemoveRenderObject(ro.id);
        }

        public void Update()
        {
            foreach (KeyValuePair<uint, TransformObject> pair in _transObjDic)
            {
                if (pair.Value.Update())
                    _completeTransObjList.AddLast(pair.Key);
            }

            foreach (uint id in _completeTransObjList)
            {
                if (this._renderObjDic.ContainsKey(id))
                    this._renderObjDic.Remove(id);
            }

            if (this._completeTransObjList.Count > 0)
                this._completeTransObjList.Clear();
        }

        public void TransformRotation(Transform trans, float angle, float time, Vector3 axis)
        {
            if (trans != null && angle != 0f && time != 0f)
            {
                uint transID = (uint)trans.GetInstanceID();
                TransformObject to = null;
                _transObjDic.TryGetValue(transID, out to);
                if (to == null)
                {
                    to = new TransformObject(trans);
                    this._transObjDic[transID] = to;
                }
                else
                {
                    to.Clear();
                }

                to.ratationTime = time;
                to.roationAxis = axis;
                to.rotationAngle = angle;
            }
        }

        private T CreateRenderObject<T>(string path) where T : RenderObject, new()
        {
            GameObject goClss = KangSingleTon<ResManager>.Inst().LoadAsset<GameObject>(ResType.RT_PREFAB, path);
            if (goClss == null)
                return default(T);

            Object go = Object.Instantiate(goClss);
            return CreateRenderObject<T>((GameObject)go);
        }

        private T CreateRenderObject<T>(GameObject obj) where T : RenderObject, new()
        {
            if (obj == null)
                return default(T);

            T local = System.Activator.CreateInstance<T>();
            uint renderObjID = KangGUID.Build();
            local.Init(renderObjID, obj);
            this._renderObjDic[renderObjID] = local;
            return local;
        }
    }
}
