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

    public void Setup(Unit character)
    {
        if(origin != null)
        {
            //origin.damaged -= OnBaseDamaged;
            //origin.changeMana -= OnChangedMana;
        }

        origin = character;
        userName.text = origin.name;
        //origin.damaged += OnBaseDamaged;
        //origin.changeMana += OnChangedMana;
        UpdateDisplay();
    }


    public void Update()
    {
        UpdateDisplay();
    }


    protected virtual void OnBaseDamaged(Unit info)
    {
        UpdateDisplay();
    }

    protected void UpdateDisplay()
    {
        if (origin == null) return;

        int currentHealth = (int)origin.currentHealth;
        health.text = currentHealth.ToString(CultureInfo.InvariantCulture);

        int currentMana = (int)origin.currentMana;
        mana.text = currentMana.ToString(CultureInfo.InvariantCulture);
    }

    protected virtual void OnChangedMana(Unit info)
    {
        UpdateDisplayMana();
    }

    protected void UpdateDisplayMana()
    {
        //if (origin == null) return; 
        //int mana = (int)origin.currentMana;
        //mana = mana.ToString(CultureInfo.InvariantCulture);
    }
}
