using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    #region Singleton
    static protected Spells s_Instance;
    static public Spells instance { get { return s_Instance; } }
    #endregion
    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
    }

    public void FireBall(SpellInfo info)
    {
        FireBallSpell ball = new FireBallSpell();
        ball.Apply(info);
    }

    public void Teleport(SpellInfo info)
    {
        MagicTeleportSpell tel = new MagicTeleportSpell();
        tel.Apply(info);
    }

}
