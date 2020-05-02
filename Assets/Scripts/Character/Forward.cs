using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    private Unit owner;
    private void Awake()
    {
        owner = GetComponent<Unit>();
    }

    void FixedUpdate()
    {
        if (owner.moveSpeed > 0)
            owner.Move(transform.forward);
        
    }
}
