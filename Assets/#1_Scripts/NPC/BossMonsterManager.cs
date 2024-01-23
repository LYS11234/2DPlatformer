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
    private int groggyGage;
    public int currentGroggyGage;
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
    Vector2 backstepForce;
    [SerializeField]
    Vector2 knockBackForce;
    #endregion
    void Start()
    {
        if(Database.Instance.nowPlayer.bossClear)
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
            TryAttack();
            TryKnockBack();
            StartCoroutine(CheckHP());
            if (hp <= 0)
                Dead();
            if(isBackStep)
                Rest();
        }
    }

    protected override void Dead()
    {
        base.Dead();
        Database.Instance.nowPlayer.bossClear = true;
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
        Debug.LogError($"Distance = {distance}");
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
                canMove = true;
                isAttack = false;
            }
        }
        else
            currentComboAttackTime += Time.deltaTime;
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
        if (distance <= 0.5f)
            StartCoroutine(KnockBackCoroutine());
    }

    private IEnumerator KnockBackCoroutine()
    {
        anim.SetTrigger("KnockBack");
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        yield return waitTime;
        PlayerManager.instance.playerRigidbody.AddForce(knockBackForce);
        
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
