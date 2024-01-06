using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public bool inventoryActivated = false;
    [SerializeField]
    public Text gold;
    [SerializeField]
    public GameObject inven_Base;
    [SerializeField]
    public Slot[] inven_Slots;

    [SerializeField]
    private Image itemDescriptionImage;
    [SerializeField]
    private Text itemDescriptionName;
    [SerializeField]
    private Text itemDescription;
    [SerializeField]
    private Text itemDescriptionItemType;
    [SerializeField]
    private Image Check;
    [SerializeField]
    private Text itemDescriptionCost;

    [SerializeField]
    public Item[] items;


    public Slot[] GetSlots() { return inven_Slots; }
    public int slotNum;
    void Start()
    {
        Database.Instance.theInven = this;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryActivated)
        {

            gold.text = Database.Instance.nowPlayer.gold.ToString();
            CheckSlotChange();
            ShowItemDescription(inven_Slots[slotNum].item);
        }
    }

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (_itemName != "")
            {
                if (items[i].itemName == _itemName)
                {
                    inven_Slots[_arrayNum].AddItem(items[i], _itemNum);
                    return;
                }
            }
            else
                return;
        }
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Potion != _item.itemType)
        {
            for (int i = 0; i < inven_Slots.Length; i++)
            {
                if (inven_Slots[i].item != null)
                {
                    if (inven_Slots[i].item.itemName == _item.itemName)
                    {
                        inven_Slots[i].SetSlotCount(_count);
                        Database.Instance.nowPlayer.itemCount[i] += _count;
                        return;
                    }
                }
            }
        }


        for (int i = 0; i < inven_Slots.Length; i++)
        {
            if (inven_Slots[i].item == null)
            {
                inven_Slots[i].AddItem(_item, _count);
                items[i] = _item;
                Database.Instance.nowPlayer.itemCount[i] = _count;
                Database.Instance.nowPlayer.items_name[i] = _item.itemName;
                return;
            }
        }
    }

    private void UseItem()
    {
        
    }

    public void ShowItemDescription(Item _item)
    {
        
        if(_item == null)
        {
            inven_Slots[slotNum].SetColor(0, itemDescriptionImage);
            itemDescriptionName.text = "";
            itemDescriptionItemType.text = "";
            itemDescription.text = "";
            itemDescriptionCost.text = "";
        }
        else
        {
            itemDescriptionImage.sprite = _item.itemImage;
            inven_Slots[slotNum].SetColor(1, itemDescriptionImage);
            itemDescriptionName.text = _item.itemName;
            itemDescriptionItemType.text = _item.itemType.ToString();
            itemDescription.text = _item.itemDescription;
            itemDescriptionCost.text = _item.value.ToString();
        }
    }

    public void CheckSlotChange()
    {
        if (inventoryActivated)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (slotNum != 0)
                    slotNum--;
                else
                {
                    slotNum = 47;
                    while (inven_Slots[slotNum].item == null)
                        slotNum--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (inven_Slots[slotNum + 1].item != null)
                    slotNum++;
                else
                    slotNum = 0;
            }
            else if(Input.GetKeyDown (KeyCode.UpArrow))
            {
                if (slotNum > 7 && inven_Slots[slotNum - 8].item != null)
                    slotNum -= 8;
            }   
            else if(Input.GetKeyDown (KeyCode.DownArrow))
            {
                if (slotNum <= 39 && inven_Slots[slotNum + 8].item != null)
                    slotNum += 8;
            }
            CheckSlot();
        }
    }

    private void CheckSlot()
    {
        Vector2 _vec = new Vector2();
        _vec.Set(inven_Slots[slotNum].transform.position.x, inven_Slots[slotNum].transform .position.y);
        Check.transform.position = _vec;
    }
}
