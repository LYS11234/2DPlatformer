using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBridge : MonoBehaviour
{
    [SerializeField]
    private BackgroundManager backGround;
    private ButtonGUI ui;

    private bool canBuild;

    private void Start()
    {
        ui = Parameter.instance.gameObject.GetComponent<ButtonGUI>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.instance.canAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.instance.canAttack = true;
            
            ui.image.gameObject.SetActive(false);
            ui.text.text = "";
        }
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

                            canBuild = true;
                            Database.Instance.nowPlayer.bridgeFixed = true;
                            backGround.bridge.SetActive(true);
                            Destroy(backGround.blockBridge);
                            Destroy(this.gameObject);
                        }
                        
                    }
                    else
                    {
                        canBuild = false;
                    }
                }
                if(!canBuild)
                {
                    ui.image.gameObject.SetActive(true);
                    ui.text.text = "You have not Enough Item.";
                }
            }
        }
    }
}
