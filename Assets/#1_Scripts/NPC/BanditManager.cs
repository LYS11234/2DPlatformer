using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditManager: HostileNPC
{
    #region Components
    [Header("# Components")]
    [SerializeField]
    private BoxCollider2D banditCol;

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
    public bool canMove = true;
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
    [Space(5)]
    #region etc
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    private WaitForEndOfFrame frameTime = new WaitForEndOfFrame();
    #endregion
    #endregion

    private void Update()
    {
        FindPlayer();
    }

}
