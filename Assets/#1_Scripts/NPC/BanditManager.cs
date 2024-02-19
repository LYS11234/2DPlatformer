using Schema;
using Schema.Builtin.Nodes;
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
    protected BoxCollider2D attackPoint;

    [SerializeField]
    private SchemaAgent schemaAgent;


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
    protected float atkTime;
    [SerializeField]
    protected float currentAtkTime;
    public bool canAttack = true;
    #endregion
    #region Find Player

    #endregion
    #region etc

    #endregion
    #endregion

    private void Start()
    {
        //schemaAgent.target.blackboard.en
        
    }


    private void FixedUpdate()
    {
        
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


    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
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
