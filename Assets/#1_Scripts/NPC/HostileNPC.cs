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
    public bool isDead;
    public bool findPlayer;
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
    protected float currentMoveCount;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float moveTime;
    [SerializeField]
    int currentDir;
    [SerializeField]
    protected float currentMoveTime;
    protected WaitForSeconds waitTime = new WaitForSeconds(0.1f);

    protected Vector2 pos = new Vector2();
    protected Vector2 dir = new Vector2();
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
        //yield return waitTime2;
        //yield return waitTime2;
        
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
        if(Physics2D.Raycast(this.transform.position, dir, 0.3f, layerMask))
        {
            findPlayer = true;
            direction = (int)(PlayerManager.instance.gameObject.transform.position.x / Math.Abs(PlayerManager.instance.gameObject.transform.position.x));
        }
        else
        {
            findPlayer = false;
            if (currentMoveCount >= moveCount)
            {
                canMove = false;
                ChangeDirection();
            }
        }
        if(canMove)
            StartCoroutine(Move(direction));
    }

    protected void ChangeDirection()
    {
        anim.SetBool("isMove", false);
        currentDir = direction;
        //direction = 0;
        if (currentMoveTime < moveTime)
        {
            currentMoveTime += Time.deltaTime;
        }
        else
        { 
            currentMoveTime = 0;
            currentMoveCount = 0;
            direction = -(currentDir);
            canMove = true;
        }
    }

    protected virtual IEnumerator Move(int _direction)
    {
        //StartCoroutine(MoveCoroutine(_direction));
        if (canMove)
        {
            anim.SetBool("isMove", true);
            if (_direction < 0)
            {
                sprite.flipX = true;
                dir = this.transform.TransformDirection(Vector2.left);
            }
            else if (_direction > 0)
            {
                sprite.flipX = false;
                dir = this.transform.TransformDirection(Vector2.right);
            }

            Vector3 _vel = transform.right * speed * _direction;
            
            currentMoveCount++;
            mobRd.MovePosition(transform.position + _vel * Time.deltaTime);
            yield return waitTime;
        }
    }

    //protected virtual private IEnumerator MoveCoroutine(int _direction)
    //{
    //    if (_direction != 0)
    //    {
    //        if (_direction < 0)
    //        {
    //            sprite.flipX = true;
    //            dir = this.transform.TransformDirection(Vector2.left);
    //        }
    //        else if (_direction > 0)
    //        {
    //            sprite.flipX = false;
    //            dir = this.transform.TransformDirection(Vector2.right);
    //        }

    //        Vector3 _vel = transform.right * speed * _direction;
    //        for (currentMoveCount = 0; currentMoveCount <= moveCount; currentMoveCount++)
    //        {
    //            mobRd.MovePosition(transform.position + _vel * Time.deltaTime);
    //            yield return waitTime;
    //        }

    //        anim.SetBool("isMove", true);
    //    }
    //}
}
