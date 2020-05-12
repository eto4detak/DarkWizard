using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseUI : MonoBehaviour
{
    public Text userName;
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    public Unit origin;

    protected virtual void Start()
    {

    }

    public void Setup(Unit character)
    {
        if(origin != null)
        {
            origin.damaged -= OnBaseDamaged;
            origin.changeMana -= OnChangedMana;
        }

        origin = character;
        userName.text = origin.name;
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
        health.text = currentHealth.ToString(CultureInfo.InvariantCulture);
    }

    protected virtual void OnChangedMana(Unit info)
    {
        UpdateDisplayMana(info);
    }

    protected void UpdateDisplayMana(Unit info)
    {
        int mana = (int)info.currentMana;
        this.mana.text = mana.ToString(CultureInfo.InvariantCulture);
    }
}
