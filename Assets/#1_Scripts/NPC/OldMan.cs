using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : AllienceNPC
{
    [SerializeField]
    private bool canTalk;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            PlayerManager.instance.canMove = false;
            StartCoroutine(DialogueCoroutine());
        }
    }

    private IEnumerator DialogueCoroutine()
    {
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        if (i < npcDialogue.Length)
        {
            dialogueManager.PrintDialogue(npcName, npcDialogue[i]);
            i++;
            yield return null;
        }
        else
        {
            dialogueManager.CloseDialogue();
            i = 0;
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
