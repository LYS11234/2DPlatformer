using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : HostileNPC
{
    public int atk;
    [SerializeField]
    protected float currentAttackTime;
    [SerializeField]
    protected float attackTime;

    protected virtual void Update()
    {
        currentAttackTime += Time.deltaTime;
    }


    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (!PlayerManager.instance.isGuard && !PlayerManager.instance.isRoll && !PlayerManager.instance.isDead)
        {
            if (currentAttackTime > attackTime)
            {
                if (col.gameObject.name == "Player")
                {
                    Parameter.instance.currentHp -= atk;
                    currentAttackTime = 0;
                }
            }
        }
        else if (PlayerManager.instance.isRoll)
        {
            if (currentAttackTime > attackTime)
            {
                if (col.gameObject.name == "Player")
                {
                    currentAttackTime = 0;
                }
            }
        }
    }
}
