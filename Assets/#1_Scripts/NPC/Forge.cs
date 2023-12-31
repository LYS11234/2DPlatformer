using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : AllienceNPC
{

    [SerializeField]
    protected Image upgradeAllow;
    [SerializeField]
    protected Image upgradeDeny;
    [SerializeField]
    private bool canTalk;


    protected override void Start()
    {
        base.Start();
        upgradeAllow = dialogueManager.upgradeAllow;
        upgradeDeny = dialogueManager.upgradeDeny;
        pointer = dialogueManager.pointer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            PlayerManager.instance.canMove = false;
            StartCoroutine(DialogueCoroutine());
        }
        if(i >= npcDialogue.Length -1 && upgradeAllow.gameObject.active)
        {
            StartCoroutine(UpgradeCoroutine());
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            CloseDialogue();
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);

    }

    private IEnumerator DialogueCoroutine()
    {
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        if (i < npcDialogue.Length)
        {
            dialogueManager.PrintDialogue("Player", npcDialogue[i]);
            i++;
            yield return null;
        }
        else
        {
            upgradeAllow.gameObject.SetActive(true);
            upgradeDeny.gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
        }
    }
    protected override void CloseDialogue()
    {
        base.CloseDialogue();
        upgradeAllow.gameObject.SetActive(false);
        upgradeDeny.gameObject.SetActive(false);
    }

    private IEnumerator UpgradeCoroutine()
    {
        yield return waitTime;
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (j == 0)
            {
                if (Database.Instance.nowPlayer.gold >= Database.Instance.nowPlayer.upgradeCost)
                {
                    Database.Instance.nowPlayer.gold -= Database.Instance.nowPlayer.upgradeCost;
                    Database.Instance.nowPlayer.additionalAtk += 1;
                    Database.Instance.nowPlayer.upgradeCost  = (int)(Database.Instance.nowPlayer.upgradeCost * 1.8f);
                    CloseDialogue();
                }
                else
                {
                    if (dialogueManager.dialogueText.text != "골드가 부족합니다.")
                    {
                        dialogueManager.dialogueText.text = "골드가 부족합니다.";
                        ChoiceCheck();
                    }
                    else
                        CloseDialogue();
                }
            }
            else if (j == 1)
            {
                j = 0;
                ChoiceCheck();
                CloseDialogue();
            }
            
        }
        else {
             if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                j = 1;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                j = 0;
            }
            ChoiceCheck();
        }
        yield return null;
    }

    

    private void ChoiceCheck()
    {
        Vector2 pos = new Vector2();
        switch(j)
        {
            case 0: 
                pos.Set(pointer.transform.position.x, upgradeAllow.transform.position.y);
                pointer.transform.position = pos;
                break;
            case 1:
                pos.Set(pointer.transform.position.x, upgradeDeny.transform.position.y);
                pointer.transform.position = pos;
                break;
            default: break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            PlayerManager.instance.canAttack = false;
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            PlayerManager.instance.canAttack = true;
            canTalk = false;
        }
    }
}
