using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    public float currentParryTime;
    [SerializeField]
    private float ParryTime;

    private void Update()
    {       
        ParryCheck();
    }

    private void ParryCheck()
    {
        currentParryTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Monster" || collider.gameObject.tag == "VillianAttack")
        {
            if (currentParryTime > ParryTime)
            {
                if(collider.gameObject.tag == "Monster")
                {
                    Monster mob = collider.gameObject.GetComponent<Monster>();
                    Parameter.instance.currentHp -= (int)(mob.atk * 0.3f);
                }
            }
            else
            {

            }
        }
    }
}
