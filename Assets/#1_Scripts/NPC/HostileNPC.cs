using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HostileNPC : NPC
{
    #region Components
    [Header("Components")]
    [SerializeField]
    protected SpriteRenderer sprite;
    [SerializeField]
    protected Rigidbody2D mobRd;
    [SerializeField]
    protected CoinManager theCoin;
    [SerializeField]
    protected MonsterHP mobHp;
    [SerializeField]
    protected GameObject item_pouch;
    [SerializeField]
    protected Item[] items;
    #endregion
    [Space(10)]
    #region Variables
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected bool isDead;
    [SerializeField]
    protected int exp;
    [SerializeField]
    protected int minGold;
    [SerializeField]
    protected int maxGold;
    #endregion

    [SerializeField]
    private int value;

    private void Start()
    {
        RandomValue();
    }
    public void Damage(float _damage)
    {
        if (!isDead)
        {
            hp -= _damage;
            if (hp <= 0)
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
        DropItem(this.transform);
        Destroy(this.gameObject);
    }

    protected void RandomValue()
    {
        value = UnityEngine.Random.Range(minGold - 1, maxGold);
    }


    protected virtual void DropItem(Transform _transform)
    {
        GameObject pouch = Instantiate(item_pouch, _transform);
        ItemPouch itemPouch = pouch.GetComponent<ItemPouch>();
        Array.Resize(ref itemPouch.items, items.Length);
        itemPouch.items = items;
        CoinManager coinValue = pouch.GetComponent<CoinManager>();
        coinValue.value = value;
        pouch.transform.SetParent(null);
    }

    protected virtual void FindPlayer()
    {
        Debug.DrawRay(mobRd.position, -this.transform.forward, new Color(0,1,0));
    }
}
