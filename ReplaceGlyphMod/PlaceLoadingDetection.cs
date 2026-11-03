

using System;
using HarmonyLib;
using ReplaceGlyphMod;

[HarmonyPatch(typeof(CutoffUI), nameof(CutoffUI.Loop))]
class CutoffUIPatch
{
	static private bool _placeIsLoading = false;

	static public event EventHandler OnNewPlace;

	static readonly AccessTools.FieldRef<CutoffUI, bool> _goToLinkedPortalRef = AccessTools.FieldRefAccess<CutoffUI, bool>("goToLinkedPortal");
	static void Prefix(CutoffUI __instance)
	{
		if (_goToLinkedPortalRef(__instance))
		{
			_placeIsLoading = true;
		}
	}

	static void Postfix(CutoffUI __instance)
	{
		if (_placeIsLoading)
		{
			var placeController = GameController.GetInstance().GetPlaceController();

			if (placeController.GetCurrentPlace().IsInit())
			{
				Plugin.Logger.LogInfo("CutoffUI place loaded: " + placeController.GetCurrentPlace().name);
				_placeIsLoading = false;

				OnNewPlace?.Invoke(null, null);
			}

		}
	}
}