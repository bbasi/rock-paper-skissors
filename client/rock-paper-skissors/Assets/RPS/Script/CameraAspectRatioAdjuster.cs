using System.Collections;
using UnityEngine;

// http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
[RequireComponent(typeof(Camera))]
public class CameraAspectRatioAdjuster : MonoBehaviour
{
    Camera camera_;

    private void Awake()
    {
        camera_ = GetComponent<Camera>();
        AdjustAspectRatio();

        StartCoroutine(_AutoAdjust());
        IEnumerator _AutoAdjust()
        {
            yield return null;
            var prv = new Vector2(Screen.width, Screen.height);

            while (true)
            {
                if (prv.x != Screen.width || prv.y != Screen.height)
                {
                    // Debug.Log("[CameraAspectRatioAdjuster._AutoAdjust] Detected screen res change, adjusting");
                    prv.x = Screen.width;
                    prv.y = Screen.height;
                    AdjustAspectRatio();
                }
                yield return null;
            }
        }
    }

    void AdjustAspectRatio()
    {
        //Debug.Log("[CameraAspectRatioAdjuster.AdjustAspectRatio] 16:10");
        const float targetaspect = 16.0f / 10.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        Rect rect = camera_.rect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
        }
        camera_.rect = rect;
    }
}
