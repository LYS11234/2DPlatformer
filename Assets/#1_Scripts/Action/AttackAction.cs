using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{

    private int layerMask; 
    private Vector2 attackSize = new Vector2(0.365417f, 0.5159765f);

    private void Start()
    {
        layerMask = (1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Default"));
        layerMask = ~layerMask;
    }
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
            float attack = 20 + Database.Instance.nowPlayer.additionalAtk;
            PlayerManager.instance.playerAnim.SetTrigger("Attack1");
            yield return PlayerManager.instance.waitTime;
            //PlayerManager.instance.attackPoint.gameObject.SetActive(true);
            RaycastHit2D hit = Physics2D.BoxCast(PlayerManager.instance.transform.position, attackSize, 0, PlayerManager.instance.playerDir, 0.365417f / 2, layerMask);

            if (hit.transform == null)
                Debug.LogError("Null");
            
            else
            {
                Debug.LogError($"BoxCast Hit: {hit.transform.name}");
                if (hit.transform.tag == "Monster")
                {
                    NPC npc = hit.transform.GetComponent<NPC>();
                    if (npc.npcType == "Monster")
                    {
                        Monster mob = npc.gameObject.GetComponent<Monster>();
                        mob.Damage(attack);
                    }
                    else if (npc.npcType == "Hostile NPC")
                    {
                        BanditManager bandit = npc.gameObject.GetComponent<BanditManager>();
                        bandit.Damage(attack);

                    }
                    else if (npc.npcType == "BossMonster")
                    {
                        BossMonsterManager bossManager = npc.gameObject.GetComponent<BossMonsterManager>();
                        bossManager.Damage(attack);
                        bossManager.currentGroggyGage += attack * 2 / 3;
                    }
                }
                
            }
            PlayerManager.instance.isCombo = true;
            yield return PlayerManager.instance.waitTime;
        }
        else if (PlayerManager.instance.currentComboTime <= PlayerManager.instance.comboTime && 0 < PlayerManager.instance.currentComboTime)
        {
            float attack = 15 + Database.Instance.nowPlayer.additionalAtk;
            PlayerManager.instance.playerAnim.SetTrigger("Attack2");
            yield return PlayerManager.instance.waitTime;
            RaycastHit2D hit = Physics2D.BoxCast(PlayerManager.instance.transform.position, attackSize, 0, PlayerManager.instance.playerDir, 0.365417f / 2, layerMask);

            if (hit.transform == null)
                Debug.LogError("Null");
            else
            {
                if (hit.transform.tag == "Monster")
                {
                    NPC npc = hit.transform.GetComponent<NPC>();
                    if (npc.npcType == "Monster")
                    {
                        Monster mob = npc.gameObject.GetComponent<Monster>();
                        mob.Damage(attack);
                    }
                    else if (npc.npcType == "Hostile NPC")
                    {
                        if (npc.TryGetComponent<BossMonsterManager>(out BossMonsterManager bossManager))
                        {
                            bossManager.Damage(attack);
                            bossManager.currentGroggyGage += attack * 2 / 3;
                        }
                        else if(npc.TryGetComponent<BanditManager>(out BanditManager bandit))
                        {
                            
                            bandit.Damage(attack);
                        }
                    }
                    Debug.LogError($"{hit.transform.gameObject.name}_2");
                }
                PlayerManager.instance.isCombo = true;
                yield return PlayerManager.instance.waitTime;
            }
            PlayerManager.instance.currentComboTime = 0f;
            PlayerManager.instance.isCombo = false;
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
