using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : AllienceNPC
{
    [SerializeField]
    private Image upgradeAllow;
    [SerializeField]
    private Image upgradeDeny;
    [SerializeField]
    private Image pointer;

    private int i;
    private int j = 0;
    protected override void Start()
    {
        base.Start();
        upgradeAllow = dialogueManager.upgradeAllow.GetComponent<Image>();
        upgradeDeny = dialogueManager.upgradeDeny.GetComponent<Image>();
        pointer = dialogueManager.pointer.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            PlayerManager.instance.canMove = false;
            StartCoroutine(DialogueCoroutine());
        }
        if(i >= npcDialogue.Length && upgradeAllow.gameObject.active && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(UpgradeCoroutine());
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
            dialogueManager.PrintDialogue("", npcDialogue[i]);
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

    private IEnumerator UpgradeCoroutine()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (j == 0)
            {
                if (Database.Instance.gold >= Database.Instance.upgradeCost)
                {
                    Database.Instance.gold -= Database.Instance.upgradeCost;
                    Database.Instance.additionalAtk += 1;
                    float dump = (float)Database.Instance.upgradeCost * 1.8f;
                    Database.Instance.upgradeCost = (int)dump;
                }
                else
                {
                    dialogueManager.dialogueText.text = "골드가 부족합니다.";
                    ChoiceCheck();
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

    private void CloseDialogue()
    {
        dialogueManager.CloseDialogue();
        upgradeAllow.gameObject.SetActive(false);
        upgradeDeny.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        i = 0;
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


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
