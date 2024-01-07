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
    [SerializeField]
    protected int direction;
    [SerializeField]
    protected int moveCount;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float moveTime;
    [SerializeField]
    protected float currentMoveTime;
    protected WaitForSeconds waitTime = new WaitForSeconds(0.1f);

    Vector2 pos = new Vector2();
    private WaitForSeconds waitTime2 = new WaitForSeconds(5f);
    private WaitForEndOfFrame frameTime = new WaitForEndOfFrame();
    #endregion

    [SerializeField]
    private int value;
    [SerializeField]
    protected bool canMove;

    [SerializeField]
    protected int layerMask;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
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
        Database.Instance.nowPlayer.currentExp = Parameter.instance.currentExp;
        StartCoroutine(DeadCoroutine());
    }

    private IEnumerator DeadCoroutine()
    {   
        canMove = false;
        mobRd.gravityScale = 0;
        col.isTrigger = true;
        DropItem(this.transform);
        yield return waitTime2;
        yield return waitTime2;
        yield return waitTime2;
        yield return waitTime2;
        
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
        //Debug.DrawRay(pos, new Vector3 (-pos.x, 0f,0f), new Color(0, 3, 0), 10);
        //Debug.Log($"Pos = {pos}");
        Physics2D.Raycast(this.transform.position, this.transform.TransformDirection(Vector2.right), 0.1f, layerMask);
        if(Physics2D.Raycast(this.transform.position, this.transform.TransformDirection(Vector2.right), 0.5f, layerMask))
            Debug.Log($"Finded Player");
    }

    protected virtual void Move(int _direction)
    {
        StartCoroutine(MoveCoroutine(_direction));
    }

    protected virtual private IEnumerator MoveCoroutine(int _direction)
    {
        if (_direction != 0)
        {
            if (_direction < 0)
                sprite.flipX = true;
            else if (_direction > 0)
                sprite.flipX = false;

            Vector3 _vel = transform.right * speed * _direction;
            for (int i = 0; i < moveCount; i++)
            {
                mobRd.MovePosition(transform.position + _vel * Time.deltaTime);
                yield return waitTime;
            }

            anim.SetBool("isMove", true);
        }
        if (_direction == 0 || moveCount == 0)
        {
            anim.SetBool("isMove", false);
        }
    }
}
