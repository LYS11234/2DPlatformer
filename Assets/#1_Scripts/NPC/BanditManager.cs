using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditManager: HostileNPC
{
    #region Components
    [Header("# Components")]


    [SerializeField]
    private Transform banditTransform;

    [SerializeField]
    private BoxCollider2D attackPoint;


    #endregion

    [Space(10)]

    #region Variables
    [Header("# Variables")]

    #region Movement
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    private bool isMove;
    #endregion
    [Space(5)]
    #region State
    [Header("State")]
    #endregion
    [Space(5)]
    #region Attack
    [Header("Attack")]
    public bool isAttack;
    [SerializeField]
    private float atkTime;
    [SerializeField]
    private float currentAtkTime;
    public bool canAttack = true;
    #endregion
    #region Find Player

    #endregion
    #region etc
    
    #endregion
    #endregion

    private void Update()
    {
        if(!isDead)
            FindPlayer();
    }

    protected override void FindPlayer()
    {
        base.FindPlayer();
        if(findPlayer)
        {
            if(Mathf.Abs(PlayerManager.instance.gameObject.transform.position.x - this.gameObject.transform.position.x)<= 0.4f)
            {
                if(PlayerManager.instance.gameObject.transform.position.x - this.gameObject.transform.position.x < 0 && direction < 0)
                {
                    canMove = false;
                    anim.SetBool("isMove", false);
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
        if(currentAtkTime < atkTime)
        {
            currentAtkTime += Time.deltaTime;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        if (currentAtkTime >= atkTime)
        {
            currentAtkTime = 0;
            anim.SetTrigger("Attack");
            yield return waitTime;
            yield return waitTime;
            yield return waitTime;
            yield return waitTime;
            yield return waitTime;

            attackPoint.gameObject.SetActive(true);
            yield return waitTime;
            attackPoint.gameObject.SetActive(false);
        }
    }

    protected override IEnumerator Move(int _direction)
    {
        return base.Move(_direction);
    }

    protected override void Dead()
    {
        anim.SetTrigger("Death");
        base.Dead();
    }
}
