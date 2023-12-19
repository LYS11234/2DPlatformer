using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllienceNPC : NPC
{
    [SerializeField]
    protected DialogueManager dialogueManager;
    [SerializeField]
    protected bool canTalk;
    [SerializeField]
    protected ButtonGUI gui;

    protected virtual void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        { 
            PlayerManager.instance.canAttack = false; 
            canTalk = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        { 
            PlayerManager.instance.canAttack = true; 
            canTalk = false;
        }
    }
}
