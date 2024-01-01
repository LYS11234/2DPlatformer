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
    private BoxCollider2D col;
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
    [SerializeField]
    protected RaycastHit2D hit;
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

    Vector2 pos = new Vector2();
    private WaitForSeconds waitTime = new WaitForSeconds(5f);
    private WaitForEndOfFrame frameTime = new WaitForEndOfFrame();
    #endregion

    [SerializeField]
    private int value;
    [SerializeField]
    protected bool canMove;

    private void Start()
    {
        RandomValue();
        pos.Set(this.transform.position.x, this.transform.position.y + 100);
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

    protected virtual void Dead()
    {
        isDead = true;
        Parameter.instance.currentExp += exp;
        Database.Instance.currentExp = Parameter.instance.currentExp;
        StartCoroutine(DeadCoroutine());
    }

    private IEnumerator DeadCoroutine()
    {   
        canMove = false;
        mobRd.gravityScale = 0;

        col.isTrigger = true;
        DropItem(this.transform);
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        
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
        Debug.DrawRay(pos, new Vector3 (-pos.x, 0f,0f), new Color(0, 3, 0), 5);
        Debug.Log($"Pos = {pos}");
    }
}
