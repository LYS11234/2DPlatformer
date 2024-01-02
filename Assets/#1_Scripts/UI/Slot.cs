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
    public Text text_Count;

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

    public void SetColor(float _alpha, Image _image)
    {
        Color color = _image.color;
        color.a = _alpha;
        _image.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        if (_item.itemType != Item.ItemType.Potion)
        {
            item = _item;
            itemCount = _count;
            itemImage.sprite = item.itemImage;
            text_Count.text = itemCount.ToString();

            SetColor(1, itemImage);
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
        SetColor(0, itemImage);

        text_Count.text = "0";
    }

    void Update()
    {
        
    }
}
