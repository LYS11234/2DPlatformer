using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAttack : MonoBehaviour
{
    [SerializeField]
    public int atk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !PlayerManager.instance.isGuard && !PlayerManager.instance.isRoll && !PlayerManager.instance.isDead && !PlayerManager.instance.isParry)
        {
            Parameter.instance.currentHp -= atk;
        }
        else if(collision.gameObject.name == "Player" && PlayerManager.instance.isGuard && !PlayerManager.instance.isRoll && !PlayerManager.instance.isDead && !PlayerManager.instance.isParry)
        {
            Parameter.instance.currentHp -= (int)(atk / 3); 
        }
    }
}
