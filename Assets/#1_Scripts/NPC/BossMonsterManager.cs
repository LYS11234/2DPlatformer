using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterManager : BanditManager
{
    #region Components
    [Header("Components")]
    [SerializeField]
    private MoveSceneNPC campfire;
    [SerializeField]
    private BossParameterManager bossParameter;

    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField]
    private string[] deadMessage;

    [SerializeField]
    private float groggyGage;
    public float currentGroggyGage;
    [SerializeField]
    private float currentGroggyTime;
    [SerializeField]
    private float groggyTime;
    [SerializeField]
    private float distance;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private float comboAttackTime;
    [SerializeField]
    private float currentComboAttackTime;
    [SerializeField]
    private float backstepTime;
    [SerializeField]
    private float currentBackstepTime;
    [SerializeField]
    private bool isBackStep;
    [SerializeField]
    private float restTime;
    [SerializeField]
    private float currentRestTime;
    [SerializeField]
    private float knockBackTime;
    [SerializeField]
    private float currentKnockBackTime;
    public bool isGroggy;

    [SerializeField]
    Vector2 backstepForce;

    #endregion
    void Start()
    {
        if(Database.Instance.nowPlayer.clearedLevel == 3)
        {
            campfire.gameObject.SetActive(true);
            bossParameter.bossHpBase.gameObject.SetActive(false);
            bossParameter.bossName.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            bossParameter.bossHpBase.gameObject.SetActive(true);
            bossParameter.bossName.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {

            SetDirection();
            StartCoroutine(CheckDistance());
            if (distance > 0.5f && canMove)
                TryMove();
            else
                anim.SetBool("isMove", false);
            TryBackStep();
            if(canAttack)
                TryAttack();
            //TryKnockBack();
            StartCoroutine(CheckHP());

            if(isBackStep)
                Rest();
            if(currentGroggyGage <= 0)
            {
                Rest();
                isGroggy = true;
                if (groggyTime <= currentGroggyTime)
                {
                    isGroggy = false;
                    currentGroggyGage = groggyGage;
                }
                else
                    currentGroggyTime += Time.deltaTime;
            }
            if (hp <= 0)
                Dead();
        }
    }

    protected override void Dead()
    {
        base.Dead();
        Database.Instance.nowPlayer.clearedLevel = 3;
        campfire.gameObject.SetActive(true);
        bossParameter.bossHpBase.gameObject.SetActive(false);
        bossParameter.bossName.gameObject.SetActive(false);
    }

    private void SetDirection()
    {
        if(!isAttack)
            direction = (int)((PlayerManager.instance.gameObject.transform.position.x - this.gameObject.transform.position.x) / Mathf.Abs(PlayerManager.instance.gameObject.transform.position.x - this.gameObject.transform.position.x) -0.01f);
    }

    private IEnumerator CheckDistance()
    {
        distance = Mathf.Abs(PlayerManager.instance.gameObject.transform.position.x - this.gameObject.transform.position.x);
        yield return waitTime;
    }

    private void TryAttack()
    {
       
        if (currentComboAttackTime >= comboAttackTime)
        {
            if (distance <= 0.5f && !isAttack && canAttack)
            {
                canMove = false;
                anim.SetBool("isMove", false);
                currentComboAttackTime = 0;
                StartCoroutine(ComboAttack());
                currentComboAttackTime = 0;

                
                
                isAttack = false;
            }
        }
        else if (currentAtkTime >= atkTime)
        {
            if (distance <= 0.5f && !isAttack && canAttack)
            {
                canMove = false;
                anim.SetBool("isMove", false);
                currentAtkTime = 0;
                StartCoroutine(AttackCoroutine());
                currentAtkTime = 0;


                canMove = true;
                isAttack = false;
            }
            
        }
        else
        {
            currentComboAttackTime += Time.deltaTime;
            currentAtkTime += Time.deltaTime;
        }

    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        anim.SetTrigger("Attack");
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;


        attackPoint.gameObject.SetActive(true);
        yield return waitTime;
        attackPoint.gameObject.SetActive(false);
        canMove = true;
    }


    private IEnumerator ComboAttack()
    {
        isAttack = true;
        anim.SetTrigger("BossAttackCombo");
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        attackPoint.gameObject.SetActive(true);
        yield return waitTime;
        attackPoint.gameObject.SetActive(false);
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        attackPoint.gameObject.SetActive(true);
        yield return waitTime;
        attackPoint.gameObject.SetActive(false);
        yield return waitTime;
        attackPoint.gameObject.SetActive(true);
        yield return waitTime;
        attackPoint.gameObject.SetActive(false);
        canMove = true;
    }

    private IEnumerator CheckHP()
    {
        bossParameter.bossHp.fillAmount = hp / maxHP;
        yield return waitTime;
    }

    private void TryBackStep()
    {
        if(hp / maxHP <= 0.5f)
        {
            if (currentBackstepTime >= backstepTime)
            {
                if (distance <= 0.5f && !isBackStep)
                {
                    canMove =false;
                    canAttack = false;
                    StartCoroutine(BackstepCoroutine());
                    currentBackstepTime = 0;
                }
            }
            else
                currentBackstepTime += Time.deltaTime;
        }
    }

    private IEnumerator BackstepCoroutine()
    {
        isBackStep = true;
        anim.SetTrigger("BackStep");
        anim.SetBool("isGround", canMove);
        mobRd.AddForce(backstepForce);        
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        anim.SetBool("isGround", true);
        if (distance < 0.5f)
            StartCoroutine(KnockBackCoroutine());

       
    }

    private void TryMove()
    {
        if (canMove)
        {
            StartCoroutine(Move(direction));
        }        
    }

    private void TryKnockBack()
    {
        if (currentKnockBackTime >= knockBackTime)
        {
            if (!PlayerManager.instance.isKnockBack && !PlayerManager.instance.isRoll)
            {
                if (distance <= 0.5f && hp <= maxHP / 2)
                {
                    StartCoroutine(KnockBackCoroutine());
                    currentKnockBackTime = 0;
                }
            }
        }
        else
            currentKnockBackTime += Time.deltaTime;

    }

    private IEnumerator KnockBackCoroutine()
    {
        anim.SetTrigger("KnockBack");
        
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        if (!PlayerManager.instance.isRoll)
        {
            if (distance <= 0.5f)
            {
                PlayerManager.instance.isKnockBack = true;
            }
        }
        yield return waitTime;
        
    }


    private void Rest()
    {
        if (isBackStep)
        {
            if (currentRestTime < restTime)
            {
                currentRestTime += Time.deltaTime;
            }
            else
            {
                currentRestTime = 0;
                isBackStep = false;
                canMove = true;
                canAttack = true;
            }
        }
    }
}
