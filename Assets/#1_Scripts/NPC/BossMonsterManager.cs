using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterManager : BanditManager
{
    #region Components
    [Header("Components")]
    

    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField]
    private string[] deadMessage;

    [SerializeField]
    private int groggyGage;
    public int currentGroggyGage;

    #endregion
    void Start()
    {
        if(Database.Instance.nowPlayer.bossClear)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
            FindPlayer();
    }

    protected override void Dead()
    {
        base.Dead();
        Database.Instance.nowPlayer.bossClear = true;
    }
}
