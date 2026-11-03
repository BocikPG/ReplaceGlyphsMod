

using System;
using System.Collections;
using System.Collections.Generic;
using ReplaceGlyphMod.GlyphSwap;
using UnityEngine;
using UnityEngine.UI;

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

        gameObject.GetComponent<BulleUI>();

        Journal = GameController.GetInstance().GetJournal();
        DevoteeLanguage = Journal.devotLanguage;
        WarriorLanguage = Journal.guerrierLanguage;
        BardLanguage = Journal.bardeLanguage;
        AlchemistLanguage = Journal.alchimisteLanguage;
        AnchoritesLanguage = Journal.reclusLanguage;

        SwapAllGlyphs();

        CutoffUIPatch.OnNewPlace += OnNewPlaceEventHandler;

    }

    private void OnNewPlaceEventHandler(object sender, EventArgs e)
    {
        

    }


    private void SwapAllGlyphs()
    {
        SwapGlyphs.SwapGlyphsSeedBased(DevoteeLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(WarriorLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(BardLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(AlchemistLanguage.keys);
        SwapGlyphs.SwapGlyphsSeedBased(AnchoritesLanguage.keys);

        SwapGlyphs.SwapBardeSpecialSprites(FindObjectsOfType<BulleUI>(true));
    }

}