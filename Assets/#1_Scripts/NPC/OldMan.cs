using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : AllienceNPC
{
    [SerializeField]
    private bool canTalk;

    [SerializeField]
    private string[] oldManDialogue1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            PlayerManager.instance.canMove = false;
            if (Database.Instance.nowPlayer.oldmanStep == 0)
            {
                if(i < npcDialogue.Length)
                    StartCoroutine(DialogueCoroutine(npcDialogue));
                else if (i >= npcDialogue.Length)
                {
                    dialogueManager.CloseDialogue();
                    i = 0;
                    Database.Instance.nowPlayer.oldmanStep = 1;
                }
            }
            else if (Database.Instance.nowPlayer.oldmanStep == 1) 
            {
                if(i < oldManDialogue1.Length)
                    StartCoroutine(DialogueCoroutine(oldManDialogue1));
                else if (i >= oldManDialogue1.Length)
                {
                    dialogueManager.CloseDialogue();
                    i = 0;
                }
            }
        }
    }

    private IEnumerator DialogueCoroutine(string[] npcDialogue)
    {
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        dialogueManager.PrintDialogue(npcName, npcDialogue[i]);
        i++;
        yield return waitTime;
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
