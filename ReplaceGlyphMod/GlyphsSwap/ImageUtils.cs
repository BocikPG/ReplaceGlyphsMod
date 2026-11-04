
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ReplaceGlyphMod.GlyphSwap;

public static class ImageUtils
{
    private static Dictionary<string, Sprite> _cachedSprites = new();

    public static void SetInGameSprite<T>(string picturePath, T obj, string fieldName, int textureWidth, int textureHeight, float pivotX, float pivotY, bool cache = false) where T : class
    {
        Sprite sprite = CreateSprite(picturePath, textureWidth, textureHeight, pivotX, pivotY, cache);

        obj.SetFieldValue(fieldName, sprite);
    }
    public static void SetInGameSprite(string picturePath, Image image, int textureWidth, int textureHeight, float pivotX, float pivotY, bool cache = false)
    {
        Sprite sprite = CreateSprite(picturePath, textureWidth, textureHeight, pivotX, pivotY, cache);

        image.sprite = sprite;
    }
    internal static void SetInGameSprite(string spritePath, Image image, TextureSize size, bool cache)
    {
        var sizeWidth = SwapGlyphs.SelectSizeVector(TextureSize.SMALL, true);
        var sizeHeight = SwapGlyphs.SelectSizeVector(TextureSize.SMALL, false);

        SetInGameSprite(spritePath, image, sizeWidth, sizeHeight, sizeWidth / 2f, sizeHeight / 2f, cache);
    }

    private static Sprite CreateSprite(string picturePath, int textureWidth, int textureHeight, float pivotX, float pivotY, bool cache)
    {
        Sprite sprite = null;
        if (cache)
        {
            if (_cachedSprites.ContainsKey(picturePath))
            {
                sprite = _cachedSprites[picturePath];
            }
        }

        if (sprite == null)
        {

            sprite = Sprite.Create(CreateTexture(picturePath, textureWidth, textureHeight, TextureFormat.RGBA32), new Rect(0, 0, textureWidth, textureHeight), new Vector2(pivotX, pivotY));

            if (cache)
            {
                _cachedSprites.Add(picturePath, sprite);
            }
        }

        return sprite;
    }

    public static Texture2D CreateTexture(string picturePath, int textureWidth, int textureHeight, TextureFormat format)
    {
        var texture = new Texture2D(textureWidth, textureHeight, format, false);
        ImageConversion.LoadImage(texture, File.ReadAllBytes(picturePath));

        texture.filterMode = FilterMode.Trilinear;

        return texture;
    }
}