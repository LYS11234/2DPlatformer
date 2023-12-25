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
    private bool findPlayer;
    #endregion
    #region etc
    
    #endregion
    #endregion

    private void Update()
    {
        FindPlayer();
    }

    protected override void Dead()
    {
        anim.SetTrigger("Death");
        base.Dead();
    }
}
