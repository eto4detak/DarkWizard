using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : Singleton<KeyController>
{
    private Unit owner;

    private void Start()
    {
        
        owner = GetComponent<Unit>();
    }
    
    void FixedUpdate()
    {

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(direction.sqrMagnitude != 0)
        {
            owner.Move(direction);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            owner.ApplySpell(new FireBallSpell());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            owner.ApplySpell(new MagicTeleportSpell());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            owner.ApplySpell(new ElectroSpell());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            owner.ApplySpell(new BombSpell());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            owner.ApplySpell(new FireWallSpell());
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            owner.ApplySpell(new RainSpell());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            owner.ApplySpell(new Dispell());
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            owner.ApplySpell(new SwordSpell());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            owner.ApplySpell(new PhobiaBallSpell());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            owner.ApplySpell(new MagicShieldSpell());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            owner.ApplySpell(new FrozenBallSpell());
        }
    }
}

