

using System.Collections;
using ReplaceGlyphMod.GlyphSwap;
using UnityEngine;

public class GlyphHandler : MonoBehaviour
{
	public Journal Journal;

	public JournalLanguageUI DevoteeLanguage;
	public JournalLanguageUI WarriorLanguage;
	public JournalLanguageUI BardLanguage;
	public JournalLanguageUI AlchemistLanguage;
	public JournalLanguageUI AnchoritesLanguage;

	public void Awake()
	{
		StartCoroutine(nameof(Init));
	}

	public IEnumerator Init()
    {
        while (GameController.GetInstance() == null || GameController.GetInstance().GetJournal() == null)
            yield return null;

        Journal = GameController.GetInstance().GetJournal();
        DevoteeLanguage = Journal.devotLanguage;
        WarriorLanguage = Journal.guerrierLanguage;
        BardLanguage = Journal.bardeLanguage;
        AlchemistLanguage = Journal.alchimisteLanguage;
        AnchoritesLanguage = Journal.reclusLanguage;

        SwapAllGlyphs();

    }

	

    private void SwapAllGlyphs()
    {
        SwapGlyphs.SwapGlyphsSeedBased(DevoteeLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(WarriorLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(BardLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(AlchemistLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(AnchoritesLanguage.keys);
    }

}