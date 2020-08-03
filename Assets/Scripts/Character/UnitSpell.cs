using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{



    public List<AMagicSpell> GetSpells()
    {
        return spells;
    }

    public float Mana
    {
        get => currentMana;
        set
        {
            currentMana = value;
            if (currentMana < 0) currentMana = 0;
            if (currentMana > maxMana) currentMana = maxMana;
            changeMana?.Invoke(this);
        }
    }

    public void ApplyAttackSpell()
    {
        var attackSpells = spells.FindAll(x => x.type == MagicType.attack);
        ApplySpell(attackSpells[UnityEngine.Random.Range(0, attackSpells.Count)]);
    }


    public void ApplySpell(AMagicSpell spell)
    {
        if (noControl) return;
        if (currentSpellTime > 0) return;
        CastSpell(spell);
    }
    public void ApplySpell(int spellNumber)
    {
        if (currentSpellTime > 0) return;
        if(spells.Count > spellNumber)
        {
            CastSpell(spells[spellNumber]);
        }
    }

    protected void MagicZone()
    {
        if (currentMana < 30)
        {
            isMagicZone = false;
        }
        else
        {
            isMagicZone = true;
        }
    }


    protected void ReplenishmentMana()
    {
        float addMana = 25f * Time.fixedDeltaTime;
        Mana += addMana;
    }

    protected bool ManaToSpell(AMagicSpell spell)
    {
        if (currentMana >= spell.mana)
        {
            Mana -= spell.mana;
            return true;
        }
        return false;
    }

    protected void CastSpell(AMagicSpell spell)
    {
        if (ManaToSpell(spell))
        {
            ChangeState(UnitState.Spell);
            SpellInfo sInfo = new SpellInfo()
            {
                owner = this,
            };

            spell.Apply(sInfo);
            currentSpellTime = spellTime;
        }
    }
}
