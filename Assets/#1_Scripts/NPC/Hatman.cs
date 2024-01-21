using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hatman : AllienceNPC
{
    [SerializeField]
    private bool canTalk;
    [SerializeField]
    protected Image talk;
    [SerializeField]
    private int k;
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
    [SerializeField]
    private bool chooseOne;
    [SerializeField]
    private GameObject buyBase;
    [SerializeField]
    private GameObject inven;
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private bool chooseTalk;
    [SerializeField]
    private bool talking;
    protected override void Start()
    {
        base.Start();
        talk = dialogueManager.talk;
        buy = dialogueManager.buy;
        sell = dialogueManager.sell;
        pointer = dialogueManager.pointer;
        buyBase = dialogueManager.buyBase;
        inven = dialogueManager.inven;
        inventory = Parameter.instance.GetComponent<Inventory>();
    }

    private void Update()
    {
        if(canTalk && Input.GetKeyDown(KeyCode.X) && !chooseOne && j == 0 && !chooseTalk)
        {
            PlayerManager.instance.canMove = false;
            StartCoroutine(DialogueCoroutine(npcDialogue));
        }
        else if (talking && chooseOne)
        {
            Choose();
        }
        else if(talking && Input.GetKeyDown(KeyCode.X) && j == 0 && !chooseOne )
        {
            StartCoroutine(DialogueCoroutine(talkDialogue));
        }
        else if (talking && Input.GetKeyDown(KeyCode.X) && j == 1 && !Database.Instance.store.storeActivated)
        {
            StartCoroutine(BuyCoroutine());
        }
        else if (talking && Input.GetKeyDown(KeyCode.X) && j == 2 && !inventory.inventoryActivated)
        {
            StartCoroutine(SellCoroutine());
        }
        if (talking && Input.GetKeyDown(KeyCode.Z))
        {
            CloseDialogue();
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

    protected override void CloseDialogue()
    {
        base.CloseDialogue();
        talk.gameObject.SetActive(false);
        buy.gameObject.SetActive(false);
        sell.gameObject.SetActive(false);
        pointer.gameObject.SetActive(false);
        inventory.inventoryActivated = false;
        Database.Instance.store.storeActivated = false;
        Database.Instance.store.sellActivated = false;
        Database.Instance.store.slotNum = 0;
        buyBase.SetActive(false);
        chooseTalk = false;
        chooseOne = false;
        talking = false;
        canTalk = true;
        i = 0;
        j = 0;
    }

    private IEnumerator SellCoroutine()
    {
        yield return waitTime;
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        if (i < sellDialogue.Length)
        {
            dialogueManager.PrintDialogue(npcName, sellDialogue[i]);
            i++;
            yield return null;
        }
        else
        {
            inven.SetActive(true);
            Database.Instance.store.sellActivated = true;
            //inventory.inventoryActivated = true;
            i = 0;
        }
    }

    private IEnumerator DialogueCoroutine(string[] _dialogue)
    {
        talking = true;
        canTalk = false;
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
            chooseOne = true;
            i = 0;
        }
    }
    private IEnumerator BuyCoroutine()
    {
        yield return waitTime;
        PlayerManager.instance.gui.image.gameObject.SetActive(false);
        if (i < buyDialogue.Length)
        {
            dialogueManager.PrintDialogue(npcName, buyDialogue[i]);
            i++;
            yield return null;
        }
        else
        {
            buyBase.SetActive(true);
            Database.Instance.store.storeActivated = true;
            i = 0;
        }
    }

    private void Choose()
    {

        if(Input.GetKeyDown(KeyCode.X))
        {
            k = 0;
            switch(j)
            {
                case 0:
                    talk.gameObject.SetActive(false);
                    buy.gameObject.SetActive(false);
                    sell.gameObject.SetActive(false);
                    pointer.gameObject.SetActive(false);
                    chooseOne = false;
                    chooseTalk = true;
                    break;
                case 1:
                    talk.gameObject.SetActive(false);
                    buy.gameObject.SetActive(false);
                    sell.gameObject.SetActive(false);
                    pointer.gameObject.SetActive(false);
                    chooseOne = false;
                    chooseTalk = false;
                    break;
                case 2:
                    talk.gameObject.SetActive(false);
                    buy.gameObject.SetActive(false);
                    sell.gameObject.SetActive(false);
                    pointer.gameObject.SetActive(false);
                    chooseOne = false;
                    chooseTalk = false;
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
        if(Input.GetKeyDown(KeyCode.Z))
            CloseDialogue();
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

}
