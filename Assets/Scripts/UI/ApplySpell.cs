using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplySpell : MonoBehaviour
{
    public int number;
    private Button btn;
    private List<IMagicSpell> addingSpells;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        if (UnitManager.instanceExists)
        {
            if(UnitManager.instance.hero != null)
            {
                UnitManager.instance.hero.ApplySpell(number);
            }
        }
    }
}
