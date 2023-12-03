using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Image menu;

    [SerializeField]
    private Image inventory;

    // Update is called once per frame
    void Update()
    {
        VisibleMenu();
    }

    private void VisibleMenu()
    {
        if (!menu.gameObject.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!inventory.gameObject.active)
                    menu.gameObject.SetActive(true);
                else
                    inventory.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.gameObject.SetActive(false);
            }
        }
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void InventoryBtn()
    {
        inventory.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
}
