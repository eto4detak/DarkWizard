using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookUI : Singleton<SpellBookUI>
{
    public GameObject bookWrapper;
    public SpellItemUI spellItem;

    private Unit owner;

    public void Setup(Unit p_owner)
    {
        owner = p_owner;
        CreateViewSpell();
    }

    private void CreateViewSpell()
    {
        var allSpell = owner.GetSpells();
        for (int i = 0; i < allSpell.Count; i++)
        {
            var spellView = Instantiate(spellItem, bookWrapper.transform);
            spellView.gameObject.SetActive(true);
            spellView.spellName.text = allSpell[i].ToString();
            spellView.Setup(i);
        }
    }
}
