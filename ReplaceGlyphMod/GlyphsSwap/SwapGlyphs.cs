

using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using ReplaceGlyphMod;
using UnityEngine;
using UnityEngine.UI;

namespace ReplaceGlyphMod.GlyphSwap;

public static class SwapGlyphs
{
	private const string MOD_PATH = "ReplaceGlyphMod";
	private const string DATA_PACK_PATH = "DataPack";
	private const string DEVOTEES_FOLDER = "Devotee";
	private const string WARRIORS_FOLDER = "Warrior";
	private const string BARDS_FOLDER = "Bard";
	private const string ALCHEMISTS_FOLDER = "Alchemist";
	private const string ANCHORITES_FOLDER = "Anchorites";
	private const string SMALL_SIZE = "100x100";
	private const string LARGE_SIZE = "252x252";
	private const string LARGE_ASSIGNED_SIZE = "252x252";
	private const string BARD_SPECIAL_SIZE = "20x100";
	public static long gameSeed = 100; //TODO: get from AP or change upon deciding on implementation


	public static void SwapGlyphsSeedBased(JournalKeyUI[] jKeyUI)
	{
		var langType = jKeyUI[0].GetLanguageType();
		var smallSizeWidth = SelectSizeVector(TextureSize.SMALL, true);
		var smallSizeHeight = SelectSizeVector(TextureSize.SMALL, false);
		var largeSizeWidth = SelectSizeVector(TextureSize.LARGE, true);
		var largeSizeHeight = SelectSizeVector(TextureSize.LARGE, false);

		foreach (var key in jKeyUI)
		{
			RuneData runeData = key.GetRuneData();

			string imagePathSmall = GetSpritePath(key.runeId, langType, TextureSize.SMALL);
			string imagePathLarge = GetSpritePath(key.runeId, langType, TextureSize.LARGE);

			try
			{
				ImageUtils.SetInGameSprite(imagePathSmall, key.GetRuneData(), "smallRune", smallSizeWidth, smallSizeHeight, smallSizeWidth / 2f, smallSizeHeight / 2f, true);
				ImageUtils.SetInGameSprite(imagePathLarge, key.GetRuneData(), "largeRune", largeSizeWidth, largeSizeHeight, largeSizeWidth / 2f, largeSizeHeight / 2f, true);
				ImageUtils.SetInGameSprite(imagePathLarge, key.GetRuneData(), "assignatedRune", largeSizeWidth, largeSizeHeight, largeSizeWidth / 2f, largeSizeHeight / 2f, true);

				Plugin.Logger.LogInfo($"Swapped {key.GetTranslation(I18nType.ENGLISH)}");
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError(ex + "\nat key.runeId");
			}

		}

	}

	public static void SwapGlyph(string spritePath, Image image, TextureSize size,bool cache = false)
	{


		ImageUtils.SetInGameSprite(spritePath, image, size, cache);

	}
	public static void SwapGlyph(string runeName, LanguageType languageType, Image image, TextureSize size,bool cache = false)
	{
		SwapGlyph(GetSpritePath(runeName, languageType, size), image, size, cache);
	}
	public static void SwapGlyph(int runeId, LanguageType languageType, Image image, TextureSize size,bool cache = false)
	{
		SwapGlyph(runeId.ToString(), languageType, image, size, cache);
	}

	public static string GetSpritePath(int runeId, LanguageType languageType, TextureSize size)
	{
		return GetSpritePath(runeId.ToString(), languageType, size);
	}
	public static string GetSpritePath(string runeId, LanguageType languageType, TextureSize size)
	{
		return Path.Combine(Paths.PluginPath, MOD_PATH, DATA_PACK_PATH, gameSeed.ToString(), SelectFolder(languageType), SelectSize(size), runeId + ".png");
	}

	internal static void SwapBardeSpecialSprites(BulleUI[] bulleList)
	{
		string leftImagePathBase = GetSpritePath("LeftSpecial", LanguageType.BARDES, TextureSize.BARD_SPECIAL);
		string rightImagePathBase = GetSpritePath("RightSpecial", LanguageType.BARDES, TextureSize.BARD_SPECIAL);

		foreach (var bulle in bulleList)
		{
			ImageUtils.SetInGameSprite(leftImagePathBase, bulle.leftBarde, 20, 100, 10f, 50f, true);
			ImageUtils.SetInGameSprite(rightImagePathBase, bulle.rightBarde, 20, 100, 10f, 50f, true);
		}

		Plugin.Logger.LogInfo($"Swapped Bard's front and end in this scene");
	}
	internal static void SwapBardeSpecialSprites(TerminalBulleUI[] bulleList)
	{
		string leftImagePathBase = GetSpritePath("LeftSpecial", LanguageType.BARDES, TextureSize.BARD_SPECIAL);
		string rightImagePathBase = GetSpritePath("RightSpecial", LanguageType.BARDES, TextureSize.BARD_SPECIAL);

		foreach (var bulle in bulleList)
		{
			ImageUtils.SetInGameSprite(leftImagePathBase, bulle.leftBarde, 20, 100, 10f, 50f, true);
			ImageUtils.SetInGameSprite(rightImagePathBase, bulle.rightBarde, 20, 100, 10f, 50f, true);
		}

		Plugin.Logger.LogInfo($"Swapped Bard's front and end in this scene");
	}


	private static string SelectFolder(LanguageType languageType)
	{
		switch (languageType)
		{
			case LanguageType.DEVOTS:
				return DEVOTEES_FOLDER;
			case LanguageType.GUERRIERS:
				return WARRIORS_FOLDER;
			case LanguageType.BARDES:
				return BARDS_FOLDER;
			case LanguageType.ALCHIMISTES:
				return ALCHEMISTS_FOLDER;
			case LanguageType.RECLUS:
				return ANCHORITES_FOLDER;
		}
		return "";
	}

	private static string SelectSize(TextureSize size)
	{
		switch (size)
		{
			case TextureSize.SMALL:
				return SMALL_SIZE;
			case TextureSize.LARGE:
				return LARGE_SIZE;
			case TextureSize.BARD_SPECIAL:
				return BARD_SPECIAL_SIZE;
		}

		return "";
	}
	public static int SelectSizeVector(TextureSize size, bool getWidth)
	{
		switch (size)
		{
			case TextureSize.SMALL:
				if (getWidth)
					return 100;
				else
					return 100;
			case TextureSize.LARGE:
				if (getWidth)
					return 252;
				else
					return 252;
			case TextureSize.BARD_SPECIAL:
				if (getWidth)
					return 20;
				else
					return 100;
		}

		return 0;
	}
}

public enum TextureSize
{
	SMALL,
	LARGE,
	BARD_SPECIAL
}