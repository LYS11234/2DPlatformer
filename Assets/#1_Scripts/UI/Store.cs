using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    public int slotNum;
    public int slotNum_Sell;
    void Start()
    {
        for (int i = 0; i < store_Slots.Length; i++)
        {
            store_Slots[i].AddItem(store_Slots[i].item, store_Slots[i].itemCount);
            if (store_Slots[i].item.cost > Database.Instance.nowPlayer.gold)
                store_Slots[i].itemImage.color = Color.red;
            else
                store_Slots[i].itemImage.color = Color.white;
            Database.Instance.nowPlayer.store_ItemCount[i] = store_Slots[i].itemCount;
        }
    }

    void Update()
    {
        if (storeActivated)
        {
            storeItemUpdate();
            gold.text = Database.Instance.nowPlayer.gold.ToString();
            CheckStoreSlotChange();
            ShowItemDescription(store_Slots[slotNum].item);
            Buy(slotNum);
        }
        if (sellActivated)
        {
            inven.gold.text = Database.Instance.nowPlayer.gold.ToString();
            CheckInvenSlotChange();
            inven.ShowItemDescription(inven.inven_Slots[slotNum_Sell].item);
            Sell(inven.slotNum);
        }
    }

    public void storeItemUpdate()
    {
        for (int i = 0; i < store_Slots.Length; i++)
        {
            if (store_Slots[i].item != null)
            {
                if (store_Slots[i].item.cost > Database.Instance.nowPlayer.gold)
                    store_Slots[i].itemImage.color = Color.red;
                else
                    store_Slots[i].itemImage.color = Color.white;
            }
        }
        //switch(Database.Instance.nowPlayer.clearedLevel)
        //{

        //}
    }
    public void CheckInvenSlotChange()
    {
        if (sellActivated)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (slotNum_Sell != 0)
                    slotNum_Sell--;
                else
                {
                    slotNum_Sell = 47;
                    while (inven.inven_Slots[slotNum_Sell].item == null)
                        slotNum_Sell--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (inven.inven_Slots[slotNum_Sell + 1].item != null)
                    slotNum_Sell++;
                else
                    slotNum_Sell = 0;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (slotNum_Sell > 7 && inven.inven_Slots[slotNum - 8].item != null)
                    slotNum_Sell -= 8;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (slotNum <= 39 && inven.inven_Slots[slotNum_Sell + 8].item != null)
                    slotNum_Sell += 8;
            }
            CheckSlot();
        }
    }

    private void Buy(int _slotNum)
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(BuyCoroutine(_slotNum));
        }
    }

    private IEnumerator BuyCoroutine(int _slotNum)
    {
        yield return waitTime;
        if (store_Slots[_slotNum].item.cost <= Database.Instance.nowPlayer.gold)
        {
            if (store_Slots[_slotNum].itemCount > 0)
            {
                store_Slots[_slotNum].itemCount--;
                Database.Instance.nowPlayer.store_ItemCount[_slotNum] = store_Slots[_slotNum].itemCount;
                Database.Instance.nowPlayer.gold -= store_Slots[_slotNum].item.cost;
                inven.AcquireItem(store_Slots[_slotNum].item);
                Debug.Log(store_Slots[_slotNum].item);
            }
            store_Slots[_slotNum].text_Count.text = store_Slots[_slotNum].itemCount.ToString();
            store_Slots[_slotNum].text_Count.color = Color.red;
        }
    }

    private void Sell(int _slotNum)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(SellCoroutine(_slotNum));
        }
    }

    private IEnumerator SellCoroutine(int _slotNum)
    {
        yield return waitTime;

       if(inven.inven_Slots[_slotNum].itemCount > 0)
        {
            inven.inven_Slots[_slotNum].itemCount--;
            Database.Instance.nowPlayer.itemCount[_slotNum] = inven.inven_Slots[_slotNum].itemCount;
            Database.Instance.nowPlayer.gold += inven.inven_Slots[_slotNum].item.value;
            inven.gold.text = Database.Instance.nowPlayer.gold.ToString();
            if (inven.inven_Slots[_slotNum].itemCount == 0)
            {
                Database.Instance.nowPlayer.items_name[_slotNum] = "";
                for (int i = _slotNum; i < inven.inven_Slots.Length - 1; i++)
                {
                    if (inven.inven_Slots[i + 1].item != null)
                    {
                        Database.Instance.nowPlayer.items_name[i] = Database.Instance.nowPlayer.items_name[i + 1];
                        Database.Instance.nowPlayer.itemCount[i] = Database.Instance.nowPlayer.itemCount[i + 1];
                        inven.inven_Slots[i].item = inven.inven_Slots[i + 1].item;
                        inven.inven_Slots[i].text_Count.text = inven.inven_Slots[i + 1].text_Count.text;
                        inven.inven_Slots[i].itemImage.sprite = inven.inven_Slots[i + 1].itemImage.sprite;
                    }
                    else
                    {
                        Database.Instance.nowPlayer.items_name[i] = "";
                        Database.Instance.nowPlayer.itemCount[i] = 0;
                        inven.inven_Slots[i].item = null;
                        inven.inven_Slots[i].text_Count.text = "";
                        inven.inven_Slots[i].itemImage.sprite = null;
                        inven.inven_Slots[i].itemImage.color = new Color(255, 255, 255, 0);
                    }
                }
                
            }
            else
                inven.inven_Slots[_slotNum].text_Count.text = inven.inven_Slots[_slotNum].itemCount.ToString();
            
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

    private void CheckStoreSlotChange()
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
