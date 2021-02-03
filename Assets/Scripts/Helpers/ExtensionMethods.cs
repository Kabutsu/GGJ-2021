using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
    /// <summary>
    /// Linearly interpolates an Image's color between colors a, b and c by t, with midpoint m.
    /// When t is 1 returns a. When t is m returns b. When t is 0 returns c.
    /// </summary>
    /// <param name="image">The image whose color will be interpolated.</param>
    /// <param name="a">The color when t is 1.</param>
    /// <param name="b">The color when t is m.</param>
    /// <param name="c">The color when t is 0.</param>
    /// <param name="m">The midpoint. Clamped between 0 and 1.</param>
    /// <param name="t">Float for combining a, b and c.</param>
    public static void LerpColor3(
        this Image image,
        Color a,
        Color b,
        Color c,
        float m,
        float t)
    {
        image.color = Color.Lerp(
            t >= m ? b : c,
            t >= m ? a : b,
            (t >= m ? t - m : t) * (1 / (t >= m ? 1 - m : m)));
    }

    public static string GetName(this PickUpType pickUpType)
    {
        switch (pickUpType)
        {
            case PickUpType.Key:
                return "Key";
            default:
                return pickUpType.ToString();
        }
    }
}
