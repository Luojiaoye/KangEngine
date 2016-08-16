using KangEngine.Animation.Interface;
namespace KangEngine.Render.Base
{
    public class AnimRenderObject : RenderObject
    {
        private IAnimationObject _animationObj;
        
        public IAnimationObject animationObj
        {
            get { return _animationObj; }
            set { _animationObj = value; }
        }
    }
}
