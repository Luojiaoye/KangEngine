using System.Collections.Generic;
using UnityEngine;
using KangEngine.Core;
using KangEngine.Animation.Interface;
using KangEngine.Animation.Base;
using KangEngine.Util;
using KangEngine.Res;
using KangEngine.Res.Base;

namespace KangEngine.Animation
{
    public class AnimationManager : KangSingleTon<AnimationManager>
    {
        private Dictionary<uint, IAnimationObject> _animationDic;

        public AnimationManager()
        {
            this._animationDic = new Dictionary<uint, IAnimationObject>();
        }

        public IAnimationObject CreateAnimationObject(GameObject ownerObj)
        {
            if (ownerObj == null)
                return null;

            Animator animator = ownerObj.GetComponent<Animator>();
            if (animator == null)
                return null;

            uint animationObjID = KangGUID.Build();
            return new AnimationObject(animationObjID, animator);
        }

        public void AddAnimationObject(IAnimationObject animationObj)
        {
            if (animationObj == null || this._animationDic.ContainsKey(animationObj.id))
                return;

            this._animationDic[animationObj.id] = animationObj;
        }

        public void RemoveAnimationObject(uint id)
        {
            if (this._animationDic.ContainsKey(id))
                this._animationDic.Remove(id);
        }

        public void RemoveAnimationObject(IAnimationObject animationObj)
        {
            if (animationObj == null)
                return;

            RemoveAnimationObject(animationObj.id);
        }

        public IAnimationObject GetAnimationObject(uint id)
        {
            IAnimationObject animationObj = null;
            this._animationDic.TryGetValue(id, out animationObj);
            return animationObj;
        }

        public bool SetAnimationObjController(uint id, string path)
        {
            IAnimationObject animationOb = this.GetAnimationObject(id);
            if (animationOb == null)
                return false;

            RuntimeAnimatorController controller = KangSingleTon<ResManager>.Inst().LoadAsset<RuntimeAnimatorController>(ResType.RT_ANIMATION, path);
            if (controller == null)
                return false;

            Animator animator = animationOb.owner as Animator;
            if (animator == null)
                return false;

            animator.runtimeAnimatorController = controller;
            return true;
        }
    }
}
