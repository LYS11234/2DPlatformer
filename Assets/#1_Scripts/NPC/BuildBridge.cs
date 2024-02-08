using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridge : MonoBehaviour
{
    [SerializeField]
    private BackgroundManager backGround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.instance.canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            PlayerManager.instance.canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                for(int i = 0; i < Database.Instance.theInven.inven_Slots.Length; i++)
                {
                    if (Database.Instance.theInven.inven_Slots[i].item.itemName == "Åë³ª¹«")
                    {
                        Database.Instance.theInven.inven_Slots[i].itemCount--;
                        if(Database.Instance.theInven.inven_Slots[i].itemCount <= 0)
                        {
                            for (int j = i; j < Database.Instance.theInven.inven_Slots.Length - 1; j++)
                            {

                                if (Database.Instance.theInven.inven_Slots[j + 1].item != null)
                                {
                                    Database.Instance.nowPlayer.items_name[j] = Database.Instance.nowPlayer.items_name[j + 1];
                                    Database.Instance.nowPlayer.itemCount[j] = Database.Instance.nowPlayer.itemCount[j + 1];
                                    Database.Instance.theInven.inven_Slots[j].item = Database.Instance.theInven.inven_Slots[j + 1].item;
                                    Database.Instance.theInven.inven_Slots[j].text_Count.text = Database.Instance.theInven.inven_Slots[j + 1].text_Count.text;
                                    Database.Instance.theInven.inven_Slots[j].itemImage.sprite = Database.Instance.theInven.inven_Slots[j + 1].itemImage.sprite;
                                    Database.Instance.theInven.inven_Slots[j].itemCount = Database.Instance.theInven.inven_Slots[j + 1].itemCount;
                                }
                                else
                                {
                                    Database.Instance.nowPlayer.items_name[j] = "";
                                    Database.Instance.nowPlayer.itemCount[j] = 0;
                                    Database.Instance.theInven.inven_Slots[j].item = null;
                                    Database.Instance.theInven.inven_Slots[j].text_Count.text = "";
                                    Database.Instance.theInven.inven_Slots[j].itemImage.sprite = null;
                                    Database.Instance.theInven.inven_Slots[j].itemImage.color = new Color(255, 255, 255, 0);
                                    break;
                                }
                            }
                            backGround.bridge.SetActive(true);
                            backGround.blockBridge.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
