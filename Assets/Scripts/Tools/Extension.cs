using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public static class Extension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        var rnd = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rnd.Next(n);

            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T GetRandom<T>(this IList<T> list)
    {
        if (list == null || list.Count == 0)
            return default;

        return list[new System.Random().Next(0, list.Count)];
    }

    public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
    {
        var color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
    }

    public static void SetAlpha(this Image image, float alpha)
    {
        var color = image.color;
        image.color = new Color(color.r, color.g, color.b, alpha);
    }

    public static void SetX(this Transform transform, float value)
    {
        transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }

    public static void SetY(this Transform transform, float value)
    {
        transform.position = new Vector3(transform.position.x, value, transform.position.z);
    }

    public static void SetZ(this Transform transform, float value)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, value);
    }
}
