using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTriger : MonoBehaviour
{
    public GameObject unit;
    private int heroLayer;

    public void Awake()
    {
        heroLayer = LayerMask.NameToLayer("Hero");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == heroLayer)
        {
            unit.gameObject.SetActive(true);
        }
    }


}
