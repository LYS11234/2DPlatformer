using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
    public bool storeActivated = false;
    public bool sellActivated = false;
    [SerializeField]
    public Text gold;
    [SerializeField]
    public GameObject store_Base;
    [SerializeField]
    public Slot[] store_Slots;

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
    private Item[] items;
    [SerializeField]
    private Inventory inven;

    
    public int slotNum;
    void Start()
    {
        for (int i = 0; i < store_Slots.Length; i++) 
        {
            store_Slots[i].AddItem(store_Slots[i].item, store_Slots[i].itemCount);
            if (store_Slots[i].item.cost > Database.Instance.nowPlayer.gold)
                store_Slots[i].itemImage.color = Color.red;
        }
    }

    void Update()
    {
        if (storeActivated)
        {
            
            gold.text = Database.Instance.nowPlayer.gold.ToString();
            CheckSlotChange();
            ShowItemDescription(store_Slots[slotNum].item);
            Buy(slotNum);
        }
        if(sellActivated)
        {

        }
    }

    public void storeItemUpdate()
    {
        switch(Database.Instance.nowPlayer.clearedLevel)
        {

        }
    }

    private void Buy(int _slotNum)
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (store_Slots[_slotNum].item.cost <= Database.Instance.nowPlayer.gold)
            {
                if (store_Slots[_slotNum].itemCount > 0)
                {
                    store_Slots[_slotNum].itemCount--;
                    Database.Instance.nowPlayer.gold -= store_Slots[_slotNum].item.cost;
                    inven.AcquireItem(store_Slots[_slotNum].item);
                }
                store_Slots[_slotNum].text_Count.text = store_Slots[_slotNum].itemCount.ToString();
                store_Slots[_slotNum].text_Count.color = Color.red;
            }
        }
    }

    private void ShowItemDescription(Item _item)
    {

        if (_item == null)
        {
            store_Slots[slotNum].SetColor(0, itemDescriptionImage);
            itemDescriptionName.text = "";
            itemDescriptionItemType.text = "";
            itemDescription.text = "";
            itemDescriptionCost.text = "";
        }
        else
        {
            itemDescriptionImage.sprite = _item.itemImage;
            store_Slots[slotNum].SetColor(1, itemDescriptionImage);
            itemDescriptionName.text = _item.itemName;
            itemDescriptionItemType.text = _item.itemType.ToString();
            itemDescription.text = _item.itemDescription;
            itemDescriptionCost.text = _item.cost.ToString();
            if (_item.cost > Database.Instance.nowPlayer.gold)
                itemDescriptionCost.color = Color.red;
            else
                itemDescriptionCost.color = Color.white;
        }
    }

    private void CheckSlotChange()
    {
        if (storeActivated)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (slotNum != 0)
                    slotNum--;
                else
                {
                    slotNum = 47;
                    while (store_Slots[slotNum].item == null)
                        slotNum--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (store_Slots[slotNum + 1].item != null)
                    slotNum++;
                else
                    slotNum = 0;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (slotNum > 7 && store_Slots[slotNum - 8].item != null)
                    slotNum -= 8;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (slotNum <= 39 && store_Slots[slotNum + 8].item != null)
                    slotNum += 8;
            }
            CheckSlot();
        }
    }

    private void CheckSlot()
    {
        Vector2 _vec = new Vector2();
        _vec.Set(store_Slots[slotNum].transform.position.x, store_Slots[slotNum].transform.position.y);
        Check.transform.position = _vec;
    }
}
