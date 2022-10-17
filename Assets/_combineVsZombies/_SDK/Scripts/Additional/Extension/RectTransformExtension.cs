using UnityEngine;

namespace Extensions.RectTransformExtension
{
    public static class RectTransformExtension
    {
        public static RectTransform Left(this RectTransform rectTransform, float x)
        {
            rectTransform.offsetMin = new Vector2(x, rectTransform.offsetMin.y);
            return rectTransform;
        }

        public static RectTransform Right(this RectTransform rectTransform, float x)
        {
            rectTransform.offsetMax = new Vector2(-x, rectTransform.offsetMax.y);
            return rectTransform;
        }

        public static RectTransform Bottom(this RectTransform rectTransform, float y)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, y);
            return rectTransform;
        }

        public static RectTransform Top(this RectTransform rectTransform, float y)
        {
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -y);
            return rectTransform;
        }
    }
}
