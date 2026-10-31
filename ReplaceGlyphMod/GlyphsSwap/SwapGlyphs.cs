

using System;
using BepInEx;
using ReplaceGlyphMod;

namespace ReplaceGlyphMod.GlyphSwap;

public static class SwapGlyphs
{
	private const string DATA_PACK_PATH = @"\ReplaceGlyphMod\DataPack\";
	private const string DEVOTEES_FOLDER = @"\Devotee\";
	private const string WARRIORS_FOLDER = @"\Warrior\";
	private const string BARDS_FOLDER = @"\Bard\";
	private const string ALCHEMISTS_FOLDER = @"\Alchemist\";
	private const string ANCHORITES_FOLDER = @"\Anchorites\";
	public static long gameSeed = 100; //TODO: get from AP or change upon deciding on implementation


	public static void SwapGlyphsSeedBased(JournalKeyUI[] jKeyUI)
	{
		string folder = SelectFolder(jKeyUI[0].GetLanguageType());

		foreach (var key in jKeyUI)
		{
			RuneData runeData = key.GetRuneData();

			string imagePath = Paths.PluginPath + DATA_PACK_PATH + gameSeed + folder + key.runeId + ".png";

			try
			{
				//ImageUtils.SetInGameSprite(imagePath, key.GetRuneData(), "smallRune", 68, 100, 34, 50);
				ImageUtils.SetInGameSprite(imagePath, key.GetRuneData(), "largeRune", 252, 252, 126f, 126f);
				ImageUtils.SetInGameSprite(imagePath, key.GetRuneData(), "assignatedRune", 252, 252, 126f, 126f);


				Plugin.Logger.LogInfo($"Swapped {key.GetTranslation(I18nType.ENGLISH)}");
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError(ex + "\nat key.runeId");
			}

		}

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