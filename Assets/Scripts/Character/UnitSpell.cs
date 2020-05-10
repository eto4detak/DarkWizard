using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public float currentMana;
    public float maxMana;
    public event Action<Unit> changeMana;

    protected float spellTime = 1f;
    protected float currentSpellTime;
    protected List<IMagicSpell> spells = new List<IMagicSpell>();


    public List<IMagicSpell> GetSpells()
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


    public void ApplySpell(IMagicSpell spell)
    {
        if (currentSpellTime > 0) return;
        CastSpell(spell);
    }
    public void ApplySpell(int spell)
    {
        if (currentSpellTime > 0) return;
        CastSpell(spells[spell]);
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
        float minimumAdd = 2f * Time.fixedDeltaTime;
        float addMana = Mana * 0.05f * Time.fixedDeltaTime;

        addMana = addMana > minimumAdd ? addMana : minimumAdd;
        Mana += addMana;
    }

    protected bool ManaToSpell(IMagicSpell spell)
    {
        if (currentMana >= spell.mana)
        {
            Mana -= spell.mana;
            return true;
        }
        return false;
    }

    protected void CastSpell(IMagicSpell spell)
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
