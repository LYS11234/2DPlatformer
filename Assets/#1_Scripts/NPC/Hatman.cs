using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Hatman : AllienceNPC
{
    [SerializeField]
    protected Image talk;
    [SerializeField]
    protected Image buy;
    [SerializeField]
    protected Image sell;
    public bool canSell;
    [SerializeField]
    protected string[] buyDialogue;
    [SerializeField]
    protected string[] talkDialogue;
    [SerializeField]
    protected string[] sellDialogue;
    protected override void Start()
    {
        base.Start();
        talk = dialogueManager.talk;
        buy = dialogueManager.buy;
        sell = dialogueManager.sell;
        pointer = dialogueManager.pointer;
    }

    private void Update()
    {
        if(canTalk && Input.GetKeyDown(KeyCode.X))
        {
            PlayerManager.instance.canMove = false;
            StartCoroutine(DialogueCoroutine(npcDialogue));
        }
        if (i >= npcDialogue.Length - 1 && talk.gameObject.active)
        {
            Choose();
        }
    }

    private IEnumerator DialogueCoroutine(string[] _dialogue)
    {
        yield return waitTime;
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        if (i < _dialogue.Length)
        {
            dialogueManager.PrintDialogue(npcName, _dialogue[i]);
            i++;
            yield return null;
        }
        else
        {
            talk.gameObject.SetActive(true);
            buy.gameObject.SetActive(true);
            sell.gameObject.SetActive(true);
            pointer.gameObject.SetActive(true);
        }
    }

    private void Choose()
    {

        if(Input.GetKeyDown(KeyCode.X))
        {
            switch(j)
            {
                case 0:
                    //if(i < npcDialogue.Length - 1 && sell.gameObject.active)
                    //    Choose();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default: break;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(j < 2)
                    j++;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(j > 0)
                    j--;
            }
            
            ChoiceCheck();
        }
    }

    private void BuyProduct()
    {

    }

    private void SellProduct()
    {

    }

    private void ChoiceCheck()
    {
        Vector2 pos = new Vector2();
        switch (j)
        {
            case 0:
                pos.Set(pointer.transform.position.x, talk.transform.position.y);
                pointer.transform.position = pos;
                break;
            case 1:
                pos.Set(pointer.transform.position.x, buy.transform.position.y);
                pointer.transform.position = pos;
                break;
            case 2:
                pos.Set(pointer.transform.position.x, sell.transform.position.y);
                pointer.transform.position = pos;
                break;
            default: break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
