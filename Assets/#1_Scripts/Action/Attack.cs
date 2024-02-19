using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float groggyAttack;

    private WaitForSeconds waitTime = new WaitForSeconds(0.3f);
    [SerializeField]
    private GameObject effect;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPC npc = collision.GetComponent<NPC>();
        if(npc == null) return;
        if (npc.npcType == "Monster" || npc.npcType == "Hostile NPC")
        {
            if(npc.npcType == "Monster")
            {
                Monster mob = collision.GetComponent<Monster>();
                
                mob.Damage(damage + Database.Instance.nowPlayer.additionalAtk);
                Quaternion rotate  = Quaternion.Euler(0, 0, 0);
                Instantiate(effect, mob.effectTransform, rotate);
                //if(effect.)
            }
            else if (npc.npcType == "Hostile NPC")
            {
                if(collision.TryGetComponent<BossMonsterManager>(out BossMonsterManager bossManager))
                {
                    if(bossManager.isGroggy == true)
                        bossManager.Damage(damage + Database.Instance.nowPlayer.additionalAtk + groggyAttack);
                    else
                    {
                        bossManager.Damage(damage + Database.Instance.nowPlayer.additionalAtk);
                        bossManager.currentGroggyGage += groggyAttack;
                    }
                }
                else
                    collision.GetComponent<BanditManager>().Damage(damage + Database.Instance.nowPlayer.additionalAtk);
            }
        }
    }

}
