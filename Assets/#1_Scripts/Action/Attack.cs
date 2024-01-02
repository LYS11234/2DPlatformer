using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private WaitForSeconds waitTime = new WaitForSeconds(0.3f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enabled");
        Debug.Log($"Attack traget: {collision.transform.name}");
        if (collision.GetComponent<NPC>().npcType == "Monster" || collision.GetComponent<NPC>().npcType == "Hostile NPC")
        {
            Debug.Log("Attack Enabled");
            if (collision != null)
            { 
                if(collision.GetComponent<NPC>().npcType == "Monster")
                {
                    collision.GetComponent<Monster>().Damage(damage + Database.Instance.nowPlayer.additionalAtk);
                }
                else if (collision.GetComponent<NPC>().npcType == "Hostile NPC")
                {
                    collision.GetComponent<BanditManager>().Damage(damage + Database.Instance.nowPlayer.additionalAtk);
                }

            }

            //this.transform.localPosition = new Vector3(this.transform.localPosition.x + 0.001f, 0.028f, 0);
            //this.transform.localPosition = new Vector3(this.transform.localPosition.x - 0.001f, 0.028f, 0);
        }
        //else
        //    return;
    }

}
