
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ReplaceGlyphMod.GlyphSwap;

public static class ImageUtils
{
	private static Dictionary<string, Sprite> _cachedSprites = new();

	public static void SetInGameSprite<T>(string picturePath, T obj, string fieldName, int textureWidth, int textureHeight, float pivotX, float pivotY, bool cache = false) where T : class
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

			sprite = Sprite.Create(CreateTexture(picturePath, textureWidth, textureHeight), new Rect(0, 0, textureWidth, textureHeight), new Vector2(pivotX, pivotY));

			if (cache)
			{
				_cachedSprites.Add(picturePath, sprite);
			}
		}

		obj.SetFieldValue(fieldName, sprite);
	}

	public static Texture2D CreateTexture(string picturePath, int textureWidth, int textureHeight)
	{
		var texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
		ImageConversion.LoadImage(texture, File.ReadAllBytes(picturePath));

		texture.filterMode = FilterMode.Trilinear;

		return texture;
	}
}