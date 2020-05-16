using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : IMagic
{

    private void OnTriggerStay(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit)
        {
            unit.Slow();
        }
    }

}
