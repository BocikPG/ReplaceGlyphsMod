using System;
using HarmonyLib;
using ReplaceGlyphMod;

namespace ReplaceGlyphMod;

[HarmonyPatch(typeof(RootMainMenu), nameof(RootMainMenu.OnClic), typeof(int))]
class MainMenuButtonPatch
{
	public static bool isInMenu = false;

	static void Prefix()
	{
		isInMenu = true;
	}
}