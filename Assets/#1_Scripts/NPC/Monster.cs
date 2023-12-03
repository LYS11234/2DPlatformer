using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : NPC
{
    #region Components
    [Header("Components")]
    [SerializeField]
    protected SpriteRenderer sprite;
    [SerializeField]
    protected Rigidbody2D mobRd;
    #endregion
    [Space(10)]
    #region Variables
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected bool isDead;
    [SerializeField]
    protected int exp;
    #endregion

    public void Damage(float _damage)
    {
        if (!isDead)
        {
            hp -= _damage;
            if(hp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    protected void Dead()
    {
        isDead = true;
        Parameter.instance.currentExp += exp;
        Destroy(this.gameObject);
    }

}
