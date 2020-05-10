using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public List<Button> spellButtons = new List<Button>();

    private List<IMagicSpell> addingSpells;
    void Start()
    {
        var hero = UnitManager.instance.hero;
        addingSpells = hero.GetSpells();
        for (int i = 0; i < spellButtons.Count; i++)
        {
            spellButtons[i].onClick.AddListener(OnBtnClick0);
        }
    }

    private void OnBtnClick0()
    {
        UnitManager.instance.hero.ApplySpell(addingSpells[0]);
    }

}
