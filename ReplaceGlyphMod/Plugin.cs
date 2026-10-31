using BepInEx;
using BepInEx.Logging;

namespace ReplaceGlyphMod;

[BepInPlugin("bocik.plugins.replaceglyphmod", "Replace Glyph Mod", "0.1.0")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin Replace Glyph Mod is loaded!");

        gameObject.AddComponent<GlyphHandler>();
    }
}
