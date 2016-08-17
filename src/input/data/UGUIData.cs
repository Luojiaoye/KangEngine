using UnityEngine;
using UnityEngine.UI;

namespace KangEngine.Input.Data
{
    public sealed class UGUIData
    {

        public static Color32 COLOR_HALF_GUITEXTURE = new Color32(255, 255, 255, 78);
        public static Color32 COLOR_HALF_SPRITE = new Color32(255, 255, 255, 150);
        public static Color32 COLOR_ZERO_ALL = new Color32(255, 255, 255, 0);

        public RectTransform touchzoneRect = null;
        public Image touchzoneImage = null;
        internal UnityEngine.Camera camera = null;

        internal void GetRectAndImage(GameObject gameObject)
        {
            touchzoneRect = gameObject.GetComponent<RectTransform>();
            touchzoneImage = gameObject.GetComponent<Image>();
        }

        internal bool CheckTouchPosition(Vector2 touchPos)
        {
            if (camera == null)
                return false;

            touchPos = camera.ScreenToWorldPoint(touchPos);

            if (touchPos.x < touchzoneRect.position.x + touchzoneRect.sizeDelta.x / 4.5f
                && touchPos.y < touchzoneRect.position.y + touchzoneRect.sizeDelta.y / 4.5f
                && touchPos.x > touchzoneRect.position.x - touchzoneRect.sizeDelta.x / 4.5f
                && touchPos.y > touchzoneRect.position.y - touchzoneRect.sizeDelta.y / 4.5f)
            {
                return true;
            }

            return false;
        }
    }
}
