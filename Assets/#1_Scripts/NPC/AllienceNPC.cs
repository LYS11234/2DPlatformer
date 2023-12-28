using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AllienceNPC : NPC
{
    [SerializeField]
    protected DialogueManager dialogueManager;
    [SerializeField]
    protected bool canTalk;
    [SerializeField]
    protected ButtonGUI gui;
    protected WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    [SerializeField]
    protected Image pointer;
    protected int i;
    [SerializeField]
    protected int j = 0;

    protected virtual void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        gui = dialogueManager.GetComponent<ButtonGUI>();
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
    protected virtual void CloseDialogue()
    {
        dialogueManager.CloseDialogue();
        
        pointer.gameObject.SetActive(false);
        i = 0;
    }
}
