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
    private GameObject inven_Base;
    [SerializeField]
    private Slot[] inven_Slots;
    [SerializeField]
    private Item[] items;
    public Slot[] GetSlots() { return inven_Slots; }
    private bool isNotPut;
    private int slotNum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryActivated)
        {
            gold.text = Database.Instance.gold.ToString();
        }
    }

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].itemName == _itemName)
                inven_Slots[_arrayNum].AddItem(items[i], _itemNum);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        
            for (int i = 0; i < inven_Slots.Length; i++)
            {
                if (inven_Slots[i].item != null)
                {
                    if (inven_Slots[i].item.itemName == _item.itemName)
                    {
                        inven_Slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        

        for (int i = 0; i < inven_Slots.Length; i++)
        {
            if (inven_Slots[i].item == null)
            {
                inven_Slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
