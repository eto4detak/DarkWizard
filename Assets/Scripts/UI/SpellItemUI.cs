using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItemUI : MonoBehaviour
{
    public int spellNumber;
    public Text spellName;

    public void ClickSpell()
    {
        if (UnitManager.instanceExists)
        {
            UnitManager.instance.hero.ApplySpell(spellNumber);
        }
    }

    public void Setup(int p_spellNumber)
    {
        spellNumber = p_spellNumber;
    }
}
