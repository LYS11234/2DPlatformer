using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler*/
{
    #region Components
    [Header("Components")]
    public Item item;
    public Image itemImage;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    //[SerializeField]
    //private ItemEffectDatabase theItemEffectDatabase;

    #endregion
    [Space(10)]
    #region Variables
    [Header("Variables")]
    public int itemCount;
    private Vector3 originPos;

    #endregion
    void Start()
    {
        originPos = transform.position;
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        if (item.itemType != Item.ItemType.Potion)
        {
            item = _item;
            itemCount = _count;
            itemImage.sprite = item.itemImage;
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();

            SetColor(1);
        }
    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    void Update()
    {
        
    }
}