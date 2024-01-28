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


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NPC>().npcType == "Monster" || collision.GetComponent<NPC>().npcType == "Hostile NPC")
        {
            if (collision != null)
            { 
                if(collision.GetComponent<NPC>().npcType == "Monster")
                {
                    collision.GetComponent<Monster>().Damage(damage + Database.Instance.nowPlayer.additionalAtk);
                }
                else if (collision.GetComponent<NPC>().npcType == "Hostile NPC")
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

}
