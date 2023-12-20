using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : NPC
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
    private GameObject item_pouch;
    [SerializeField]
    private Item[] items;
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
        DropItem(this.transform);
        Destroy(this.gameObject);
    }

    protected void RandomValue()
    {
        value = Random.Range(minGold-1, maxGold);
    }


    protected virtual void DropItem(Transform _transform)
    {
        GameObject pouch = Instantiate(item_pouch, _transform);
        ItemPouch itemPouch = pouch.GetComponent<ItemPouch>();
        itemPouch.items.AddRange(items);
        CoinManager coinValue = pouch.GetComponent<CoinManager>();
        coinValue.value = value;
        pouch.transform.SetParent(null);
    }
}
