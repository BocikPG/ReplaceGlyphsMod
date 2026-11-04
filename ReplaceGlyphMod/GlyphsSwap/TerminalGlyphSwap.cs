

using System.Collections.Generic;
using HarmonyLib;

namespace ReplaceGlyphMod.GlyphSwap;

[HarmonyPatch(typeof(TerminalLinkUI), nameof(TerminalLinkUI.Display), typeof(bool), typeof(bool))]
public class TerminalLinkUIPatch
{
	static void Postfix()
	{
		Plugin.Logger.LogInfo("Swapping terminal glyphs");

		var terminalLink = Plugin.FindObjectOfType<TerminalLinkUI>(true);

		var terminalDef = terminalLink.GetFieldValue<TerminalDefinition>("terminalDef");

		LanguageType langTypeLeft = terminalDef.languageA;
		LanguageType langTypeRight = terminalDef.languageB;

		SwapGlyphs.SwapBardeSpecialSprites(terminalLink.bullesRight);
		SwapGlyphs.SwapBardeSpecialSprites(terminalLink.bullesLeft);

		for (int i = 0; i < terminalDef.speechs.Length; i++)
		{

			if (terminalDef.speechs[i].rightSpeaking)
			{
				var bulle = terminalLink.bullesRight[4 - i];

				var whiteRunes = bulle.GetFieldValue<List<TerminalWhiteRuneUI>>("whiteRunes");
				var blackRunes = bulle.GetFieldValue<List<TerminalBlackRuneUI>>("blackRunes");

				for (int j = 0; j < blackRunes.Count; j++)
				{
					Plugin.Logger.LogInfo("Black rune swapped: " + terminalDef.speechs[i].runesA[j]);
					SwapGlyphs.SwapGlyph(terminalDef.speechs[i].runesA[j], langTypeLeft, blackRunes[j].runeImg, TextureSize.LARGE, true);
				}
				for (int j = 0; j < whiteRunes.Count; j++)
				{
					Plugin.Logger.LogInfo("White rune swapped: " + terminalDef.speechs[i].runesA[j]);
					SwapGlyphs.SwapGlyph(terminalDef.speechs[i].runesB[j], langTypeRight, whiteRunes[j].runeImg, TextureSize.SMALL, true);
				}
			}
			else
			{
				var bulle = terminalLink.bullesLeft[4 - i];

				var whiteRunes = bulle.GetFieldValue<List<TerminalWhiteRuneUI>>("whiteRunes");
				var blackRunes = bulle.GetFieldValue<List<TerminalBlackRuneUI>>("blackRunes");

				for (int j = 0; j < blackRunes.Count; j++)
				{
					Plugin.Logger.LogInfo("Black rune swapped: " + terminalDef.speechs[i].runesA[j]);
					SwapGlyphs.SwapGlyph(terminalDef.speechs[i].runesB[j], langTypeRight, blackRunes[j].runeImg, TextureSize.LARGE, true);
				}
				for (int j = 0; j < whiteRunes.Count; j++)
				{
					Plugin.Logger.LogInfo("White rune swapped: " + terminalDef.speechs[i].runesA[j]);
					SwapGlyphs.SwapGlyph(terminalDef.speechs[i].runesA[j], langTypeLeft, whiteRunes[j].runeImg, TextureSize.SMALL, true);
				}
			}

			Plugin.Logger.LogInfo("Swapped terminal sprites for speech:" + i);
		}
	}
}