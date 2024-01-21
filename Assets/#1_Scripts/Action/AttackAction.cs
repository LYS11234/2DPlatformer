using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    private void Update()
    {
        if (!PlayerManager.instance.isDead)
        {
            TryAttack();
            ComboCheck();
            TryGuard();
        }
    }
    private void TryAttack()
    {
        if (PlayerManager.instance.currentAtkTime >= PlayerManager.instance.atkTime)
        {
            if (Input.GetKeyDown(KeyCode.X) && !PlayerManager.instance.isMove && !PlayerManager.instance.isGuard && PlayerManager.instance.parameter.currentSp > 0 && PlayerManager.instance.canAttack)
            {
                PlayerManager.instance.isAttack = true;
                StartCoroutine(Attack());
                PlayerManager.instance.currentAtkTime = 0f;
                PlayerManager.instance.parameter.currentSp -= 20;

            }

        }
        else
            PlayerManager.instance.currentAtkTime += Time.smoothDeltaTime;
    }

    private IEnumerator Attack()
    {


        if (PlayerManager.instance.currentComboTime == 0f)
        {
            PlayerManager.instance.playerAnim.SetTrigger("Attack1");
            yield return PlayerManager.instance.waitTime;
            PlayerManager.instance.attackPoint.gameObject.SetActive(true);
            PlayerManager.instance.isCombo = true;
            yield return PlayerManager.instance.waitTime;
            PlayerManager.instance.attackPoint.gameObject.SetActive(false);
        }
        else if (PlayerManager.instance.currentComboTime <= PlayerManager.instance.comboTime && 0 < PlayerManager.instance.currentComboTime)
        {
            PlayerManager.instance.playerAnim.SetTrigger("Attack2");
            PlayerManager.instance.attackPoint2.gameObject.SetActive(true);
            yield return PlayerManager.instance.waitTime;
            PlayerManager.instance.currentComboTime = 0f;
            PlayerManager.instance.isCombo = false;
            PlayerManager.instance.attackPoint2.gameObject.SetActive(false);
        }
        PlayerManager.instance.isAttack = false;

    }

    private void ComboCheck()
    {
        if (PlayerManager.instance.isCombo)
        {
            PlayerManager.instance.currentComboTime += Time.smoothDeltaTime;
        }
        if (PlayerManager.instance.currentComboTime >= PlayerManager.instance.comboTime)
        {
            PlayerManager.instance.isCombo = false;
            PlayerManager.instance.currentComboTime = 0f;
        }
    }
    private void TryGuard()
    {
        if (PlayerManager.instance.currentGuardTime >= PlayerManager.instance.guardTime)
        {
            if (Input.GetKeyDown(KeyCode.C) && !PlayerManager.instance.isMove && PlayerManager.instance.canAttack)
            {
                PlayerManager.instance.currentParryTime = 0f;
                Guard();
                PlayerManager.instance.currentGuardTime = 0f;
            }

        }


        else
            PlayerManager.instance.currentGuardTime += Time.deltaTime;
        if (PlayerManager.instance.currentParryTime >= PlayerManager.instance.parryTime)
            PlayerManager.instance.isParry = false;
        else
        {
            PlayerManager.instance.currentParryTime += Time.deltaTime;
            PlayerManager.instance.isParry = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {

            GuardCancel();
        }
    }

    private void Guard()
    {
        if (PlayerManager.instance.currentGuardTime <= PlayerManager.instance.guardTime)
            PlayerManager.instance.isParry = true;
        else
            PlayerManager.instance.isParry = false;
        PlayerManager.instance.isGuard = true;
        PlayerManager.instance.playerAnim.SetTrigger("Block");
        PlayerManager.instance.playerAnim.SetBool("IdleBlock", true);
    }

    private void GuardCancel()
    {
        PlayerManager.instance.isGuard = false;

        PlayerManager.instance.playerAnim.SetBool("IdleBlock", false);
    }
}
