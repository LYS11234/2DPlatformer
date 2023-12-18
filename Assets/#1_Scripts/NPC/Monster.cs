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
    [SerializeField]
    protected CoinManager theCoin;
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
    private GameObject coinObj;
    [SerializeField]
    private GameObject[] dropItem;
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
        DropItem();
        DropGold(this.transform);
        Destroy(this.gameObject);
    }

    protected void RandomValue()
    {
        value = Random.Range(minGold-1, maxGold);
    }

    public void DropGold(Transform _transform)
    {
        GameObject coin = Instantiate(coinObj, _transform);
        CoinManager coinValue = coin.GetComponent<CoinManager>();
        coinValue.value = value;
        coin.transform.SetParent(null);
    }

    protected void DropItem()
    {
        GameObject dropTem = Instantiate(dropItem[0], this.transform);
        dropTem.transform.SetParent(null);
    }
}
