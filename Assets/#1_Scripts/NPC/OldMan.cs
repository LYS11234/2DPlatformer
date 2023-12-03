using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : AllienceNPC
{
    private int i;
    private void Start()
    {
        i = 0;
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.transform.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                gui.image.gameObject.SetActive(false);
                if (i < npcDialogue.Length)
                {
                    dialogueManager.PrintDialogue(npcName, npcDialogue[i]);
                    i++;
                }
                else
                {
                    dialogueManager.CloseDialogue();
                    i = 0;
                }
            }
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
