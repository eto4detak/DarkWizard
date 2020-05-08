using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    public Unit origin;
    public TextMeshProUGUI display;
    public TextMeshProUGUI manaDisplay;

    protected float m_MaxHealth;

    protected virtual void Start()
    {
        origin.damaged += OnBaseDamaged;
        origin.changeMana += OnChangedMana;
        UpdateDisplay(origin);
    }

    protected virtual void OnBaseDamaged(Unit info)
    {
        UpdateDisplay(info);
    }

    protected void UpdateDisplay(Unit info)
    {
        float currentHealth = info.currentHealth;
        display.text = currentHealth.ToString(CultureInfo.InvariantCulture);
    }

    protected virtual void OnChangedMana(Unit info)
    {
        UpdateDisplayMana(info);
    }

    protected void UpdateDisplayMana(Unit info)
    {
        int mana = (int)info.currentMana;
        manaDisplay.text = mana.ToString(CultureInfo.InvariantCulture);
    }
}
