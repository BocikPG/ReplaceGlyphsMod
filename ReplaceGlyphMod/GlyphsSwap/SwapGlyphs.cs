

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
		string folder = SelectFolder(jKeyUI[0].GetLanguageType());

		foreach (var key in jKeyUI)
		{
			RuneData runeData = key.GetRuneData();

			string imagePathBase = Path.Combine(Paths.PluginPath, MOD_PATH, DATA_PACK_PATH, gameSeed.ToString(), folder);

			string imagePathSmall = Path.Combine(imagePathBase, SMALL_SIZE, key.runeId + ".png");
			string imagePathLarge = Path.Combine(imagePathBase, LARGE_SIZE, key.runeId + ".png");

			try
			{
				ImageUtils.SetInGameSprite(imagePathSmall, key.GetRuneData(), "smallRune", 100, 100, 50f, 50f);
				ImageUtils.SetInGameSprite(imagePathLarge, key.GetRuneData(), "largeRune", 252, 252, 126f, 126f);
				ImageUtils.SetInGameSprite(imagePathLarge, key.GetRuneData(), "assignatedRune", 252, 252, 126f, 126f);

				Plugin.Logger.LogInfo($"Swapped {key.GetTranslation(I18nType.ENGLISH)}");
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError(ex + "\nat key.runeId");
			}

		}

	}

	internal static void SwapBardeSpecialSprites(BulleUI[] bulleList)
	{		
		string leftImagePathBase = Path.Combine(Paths.PluginPath, MOD_PATH, DATA_PACK_PATH, gameSeed.ToString(), BARDS_FOLDER, BARD_SPECIAL_SIZE, "LeftSpecial.png");
		string rightImagePathBase = Path.Combine(Paths.PluginPath, MOD_PATH, DATA_PACK_PATH, gameSeed.ToString(), BARDS_FOLDER, BARD_SPECIAL_SIZE, "RightSpecial.png");

		foreach(var bulle in bulleList)
		{
			ImageUtils.SetInGameSprite(leftImagePathBase, bulle.leftBarde, 20, 100, 10f, 50f);
			ImageUtils.SetInGameSprite(rightImagePathBase, bulle.rightBarde, 20, 100, 10f, 50f);
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
}