using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region Components
    [Header("Components")]

    #endregion
    [Space(10)]
    #region Variables
    public float hp;
    public bool isDead;
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
    }

}
