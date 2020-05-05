using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEvasionTactics : IAITactics
{
    public float need;

    private float evasionDirection = 0;
    private float turnTime = 2f;
    private float evasionNeed = 10f;
    private float currentTime;
    private bool closeBorder;
    private Unit owner;
    private IMagic dangerSpell;

    public SpellEvasionTactics(Unit p_ownwer)
    {
        owner = p_ownwer;
    }

    public void Control()
    {
        if (evasionDirection == 0)
        {
            evasionDirection = Random.Range(-1, 1);
            if (evasionDirection > -1) evasionDirection = 1;
        }

        Vector3 evasionDirect = Vector3.Cross(dangerSpell.direction, Vector3.up) * evasionDirection;

        Debug.Log("evasionDirect " + evasionDirect);

        owner.Move(evasionDirect);
    }

    public float CheckNeed()
    {
        dangerSpell = null;
        need = -1;
        float dangerAngle = 20f;
        float dangerDistance = 5f;
        List<IMagic> magics = MagicManager.instance.magics;
        
        for (int i = 0; i < magics.Count; i++)
        {
            float magicDistance = (magics[i].transform.position - owner.transform.position).magnitude;

            if ( magics[i].type == MagicType.attack && magicDistance < dangerDistance)
            {
                Vector3 dangerDirection = owner.transform.position - magics[i].transform.position;
                dangerDirection.y = magics[i].direction.y;
                float attackAngle = Vector3.Angle(dangerDirection, magics[i].direction);
                if(attackAngle < dangerAngle && magics[i].damage > need)
                {
                    dangerSpell = magics[i];
                    need = magics[i].damage;
                }
            }
        }
        if(need == -1)
        {
            evasionDirection = 0;
        }
        return need;
    }
}
